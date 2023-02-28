#nullable disable

using Newtonsoft.Json;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using HortBot.Model;
using HortBot.Model.Kids;
using HortBot.Model.Presences;
using HortBot.Service;
using Microsoft.Extensions.Options;

namespace HortBot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Config _telegramBotConfig;

        private string chatIdFilePath = string.Empty;

        private List<long> ChatIds = new();
        private List<PresencesPerUser> presencesPerUsers = new();

        private TelegramBotClient botClient;

        public Worker(ILogger<Worker> logger, IOptions<Config> configuration)
        {
            _telegramBotConfig = configuration.Value;

            chatIdFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), "ChatIds.json");

            if (System.IO.File.Exists(chatIdFilePath))
                ChatIds = JsonConvert.DeserializeObject<List<long>>(System.IO.File.ReadAllText(chatIdFilePath));

            botClient = new TelegramBotClient(_telegramBotConfig.TelegramBotToken);

            using CancellationTokenSource cts = new();

            ReceiverOptions receiverOptions = new()
            {
                //ThrowPendingUpdates = false,
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = botClient.GetMeAsync().Result;

            _logger = logger;

            _logger.LogInformation($"Ich bin der Bot {me.Username}");
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Message is not { } message)
                return;

            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;

            if (message.Entities.Length == 1 && message.EntityValues.First() == "/start")
            {
                Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Willkommen im Hort-Bot von Denis",
                cancellationToken: cancellationToken);

                if (ChatIds.Find(x => x == chatId) == 0) { ChatIds.Add(chatId); }

                System.IO.File.WriteAllText(chatIdFilePath, JsonConvert.SerializeObject(ChatIds));
            }

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
        }

        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var api = new HortApi(_telegramBotConfig);
            var kid = await api.GetObjectAsync<Kids>("https://elternportal.hortpro.de/api/kids");

            if (kid != null && kid.Success)
            {
                foreach (var chatid in ChatIds)
                {
                    //var chat = await botClient.GetChatAsync(chatid);
                    Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatid,
                    text: "Der Hort-Bot wurde neugestartet");
                }

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                        var presences = await api.GetObjectAsync<Presences>($"https://elternportal.hortpro.de/api/kids/{kid.Data[0].Id}/presences?start=0&limit=5");

                        var today = presences.Data.Rows.Find(x => x.DateStart.Date.OnlyDate() == DateTime.Now.Date.OnlyDate());

                        var StartDate = today?.DateStart?.Date;
                        var EndDate = today?.DateEnd?.Date;




                        foreach (var chatid in ChatIds)
                        {
                            var p = presencesPerUsers.Find(x => x.ChatId == chatid);
                            if (p == null)
                            {
                                presencesPerUsers.Add(new PresencesPerUser { ChatId = chatid });
                                p = presencesPerUsers.Find(x => x.ChatId == chatid);
                            }

                            p.DateStart = StartDate;
                            p.DateEnd = EndDate;

                            if (StartDate == null)
                                p.StartMsgSent = false;

                            if (EndDate == null)
                                p.EndMsgSent = false;

                            if (p.DateStart != null && p.DateEnd == null && (p.StartMsgSent != true))
                            {
                                Message sentMessage = await botClient.SendTextMessageAsync(
                                chatId: chatid,
                                text: $"{kid.Data[0].FirstName} ist seit {p.DateStart:HH:mm} Uhr im Hort");

                                p.StartMsgSent = true;
                            }
                            else if (p.DateStart != null && p.DateEnd != null && (p.EndMsgSent != true))
                            {
                                Message sentMessage = await botClient.SendTextMessageAsync(
                                chatId: chatid,
                                text: $"{kid.Data[0].FirstName} ist um {p.DateEnd:HH:mm} Uhr losgefahren");

                                p.EndMsgSent = true;
                            }
                            else if (p.DateStart == null && p.DateEnd == null)
                            {
                                //Message sentMessage = await botClient.SendTextMessageAsync(
                                //chatId: chatid,
                                //text: $"{kid.Data[0].FirstName} ist nicht im Hort");
                            }
                        }

                        await Task.Delay(60_000, stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Worker Exception at: {time}", DateTimeOffset.Now);
                    }
                }
            }
        }
    }
}