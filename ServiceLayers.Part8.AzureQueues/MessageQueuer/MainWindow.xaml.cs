using System;
using System.Windows;
using Definitions;
using Microsoft.Azure;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace MessageQueuer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly string _connectionString;

        private NamespaceManager _namespaceManager;

        private NamespaceManager NsManager
        {
            get {
                return _namespaceManager ??
                       (_namespaceManager = NamespaceManager.CreateFromConnectionString(_connectionString));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            _connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
        }

        private void btnPostMessage_Click(object sender, RoutedEventArgs e)
        {
            var selectedValue = cmbPostType.SelectedIndex;

            switch (selectedValue)
            {
                case 0:
                    PostMessageToQueue();
                    break;
                default:
                    PostMessageToTopic(selectedValue);
                    break;
            }
        }

        #region Queue

        private void PostMessageToQueue()
        {
            CreateQueueIfNotExists();

            var client = QueueClient.CreateFromConnectionString(_connectionString, MyConventions.QueueName);

            var message = new BrokeredMessage(txtMessage.Text);

            message.Properties[MyConventions.PropertyName] = txtProperty.Text;

            client.Send(message);

            client.Close();
        }

        private void CreateQueueIfNotExists()
        {
            if (NsManager.QueueExists(MyConventions.QueueName))
                return;

            var qd = new QueueDescription(MyConventions.QueueName)
            {
                MaxSizeInMegabytes = 5120,
                DefaultMessageTimeToLive = new TimeSpan(1, 0, 0)
            };
            NsManager.CreateQueue(qd);
        }

        #endregion

        #region Topic

        private void PostMessageToTopic(int filterValue)
        {
            CreateTopicIfNotExists();

            var client = TopicClient.CreateFromConnectionString(_connectionString, MyConventions.TopicName);

            var message = new BrokeredMessage(txtMessage.Text);

            message.Properties[MyConventions.TopicFilterProperty] = filterValue;
            
            client.Send(message);
        }

        private void CreateTopicIfNotExists()
        {
            if (NsManager.TopicExists(MyConventions.TopicName))
                return;

            // Create the Topic
            var td = new TopicDescription(MyConventions.TopicName)
                {
                    MaxSizeInMegabytes = 5120,
                    DefaultMessageTimeToLive = new TimeSpan(1, 0, 0)
                };
            NsManager.CreateTopic(td);

            // A Subscription that gets all messages.
            NsManager.CreateSubscription(MyConventions.TopicName, MyConventions.SubscriptionAll);

            // A Subscription 
            var messageFilter = new SqlFilter($"{MyConventions.TopicFilterProperty} > 1");
            NsManager.CreateSubscription(MyConventions.TopicName, MyConventions.SubscriptionFiltered, messageFilter);
        }

        #endregion

        
    }
}
