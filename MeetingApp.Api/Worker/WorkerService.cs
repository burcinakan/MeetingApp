using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MeetingApp.Api.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MeetingApp.Api.Worker
{
	public class WorkerService : BackgroundService
	{
		private readonly IServiceScopeFactory _serviceScopeFactory;

		public WorkerService(IServiceScopeFactory serviceScopeFactory)
		{
			_serviceScopeFactory = serviceScopeFactory;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				using (var scope = _serviceScopeFactory.CreateScope())
				{
					var meetingService = scope.ServiceProvider.GetRequiredService<IMeetingService>();

					var canceledMeetings = meetingService.GetCanceledMeetingsAsync();

					if (canceledMeetings.Any())
					{
						await meetingService.DeleteCanceledMeetingsAsync(canceledMeetings);
					}
				}

				await Task.Delay(5000, stoppingToken);
			}
		}
	}
}
