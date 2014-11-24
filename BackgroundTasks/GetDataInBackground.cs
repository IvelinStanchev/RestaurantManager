using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Networking.PushNotifications;

namespace BackgroundTasks
{
    public sealed class GetDataInBackground : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral _deferral = taskInstance.GetDeferral();

            RawNotification notification = (RawNotification)taskInstance.TriggerDetails;
            string content = notification.Content;

            await GetData();

            _deferral.Complete();
        }

        private async Task GetData()
        {
            var query = ParseObject.GetQuery("Order");
            IEnumerable<ParseObject> results = await query.FindAsync();
        }
    }
}
