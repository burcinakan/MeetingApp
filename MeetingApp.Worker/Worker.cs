using MeetingApp.Api.Repositories;
using MeetingApp.Api.Services;
using MeetingApp.Model.Enums;

namespace MeetingApp.Worker
{
	public class Worker : BackgroundService
	{
		private readonly IMeetingRepository _meetingRepository;

		public Worker(IMeetingRepository meetingService)
		{
			_meetingRepository = meetingService;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				var canceledMeetings = _meetingRepository.GetAll().Select(x => x.Status == DataStatus.Canceled);

				if (canceledMeetings.Any())
				{
					await _meetingRepository.DeleteCanceledMeetingsAsync(canceledMeetings);
				}


				await Task.Delay(5000, stoppingToken);
			}
		}

		public override Task StartAsync(CancellationToken cancellationToken)
		{
			
			return base.StartAsync(cancellationToken);
		}

		public override Task StopAsync(CancellationToken cancellationToken)
		{
			return base.StopAsync(cancellationToken);
		}
	}
}
