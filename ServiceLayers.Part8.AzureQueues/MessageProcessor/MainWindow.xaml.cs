using System;
using System.Text;
using System.Windows;
using Definitions;
using Microsoft.Azure;
using Microsoft.ServiceBus.Messaging;

namespace MessageProcessor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly string _connectionString;

        public MainWindow()
        {
            InitializeComponent();
            _connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
        }

        private void btnGetMessage_Click(object sender, RoutedEventArgs e)
        {
            var selectedValue = cmbPostType.SelectedIndex;

            switch (selectedValue)
            {
                case 0:
                    GetMessageFromQueue();
                    break;
                case 1:
                    GetMessageFromTopic(MyConventions.SubscriptionAll);
                    break;
                case 2:
                    GetMessageFromTopic(MyConventions.SubscriptionFiltered);
                    break;
            }
        }

        private void GetMessageFromQueue()
        {
            var client = QueueClient.CreateFromConnectionString(_connectionString, MyConventions.QueueName);

            BrokeredMessage message = client.Receive(new TimeSpan(0, 0, 0, 5));

            if (message != null)
            {
                try
                {
                    var content = message.GetBody<MessageContent>();
                    // Show the message
                    var sb = new StringBuilder();
                    sb.AppendLine("Body:");
                    sb.AppendLine(content.TextContent);
                    sb.AppendLine("");
                    sb.AppendLine("MessageID:");
                    sb.AppendLine(message.MessageId);
                    sb.AppendLine("");
                    sb.AppendLine("Custom Property:");
                    sb.AppendLine(message.Properties[MyConventions.PropertyName].ToString());

                    txtMessage.Text = sb.ToString();

                    // Remove message from queue
                    message.Complete();
                }
                catch (Exception)
                {
                    message.Abandon();
                }
            }
            else
            {
                txtMessage.Text = $"No Message in queue: {MyConventions.QueueName}";
            }

            client.Close();
        }

        private void GetMessageFromTopic(string subscription)
        {
            var client = SubscriptionClient.CreateFromConnectionString(_connectionString, MyConventions.TopicName, subscription);

            BrokeredMessage message = client.Receive(new TimeSpan(0, 0, 0, 5));

            if (message != null)
            {
                try
                {
                    // Show the message
                    var sb = new StringBuilder();
                    sb.AppendLine("Body:");
                    sb.AppendLine(message.GetBody<string>());
                    sb.AppendLine("");
                    sb.AppendLine("MessageID:");
                    sb.AppendLine(message.MessageId);
                    sb.AppendLine("");
                    sb.AppendLine("Filter Property:");
                    sb.AppendLine(message.Properties[MyConventions.TopicFilterProperty].ToString());

                    txtMessage.Text = sb.ToString();

                    // Remove message from queue
                    message.Complete();
                }
                catch (Exception)
                {
                    message.Abandon();
                }
            }
            else
            {
                txtMessage.Text = $"No Message in subscription: {subscription}";
            }
            client.Close();
        }
    }
}
