using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintJournal.ViewModels
{
	public class FallenRainViewModel
	{

		#region [ Fields ]

		private readonly MainViewModel VM;

		#endregion

		#region [ Properties ]


		#endregion

		#region [ Construction ]

		public FallenRainViewModel(MainViewModel mainVM)
		{
			VM = mainVM;
		}

		#endregion

		#region [ Public methods ]

		public void ShowReport()
		{
			//OpenedArticlesWindow view = new OpenedArticlesWindow(this)
			//{
			//	Left = VM.View.Left + 100,
			//	Top = VM.View.Top + 20,
			//};
			//View = view;

			CollectFallenRain();
			//View.Show();
		}

		#endregion

		private void CollectFallenRain()
		{
			var rain = VM.Journals
				.Where(x => x.Event == "Regen")
				.Select((Date, Rain) => 
					new
					{ 
						Date = Date.DTStart.Value,
						Rain = decimal.Parse(Date.Message)
					})
				.GroupBy(rain => rain.Date)
				;
		}

	}
}
