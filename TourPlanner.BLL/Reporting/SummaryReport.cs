using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using TourPlanner.Models;

namespace TourPlanner.BLL.Reporting
{
    class SummaryReport : IDocument
    {
        private List<LogItem> _allLogs;

        public SummaryReport(List<LogItem> allLogs)
        {
            this._allLogs = allLogs;
        }

        public DocumentMetadata GetMetadata()
        {
            return new DocumentMetadata() { Title = DateTime.Now + "_SummaryReport" };
        }

        public void Compose(IContainer container)
        {
            container.Row(row =>
            {




                row.RelativeColumn().Stack(stack =>
                {
                    var totalTotalTime = new TimeSpan();
                    var totalDistance = 0.0;

                    foreach (var log in _allLogs)
                    {
                        totalTotalTime += log.TotalTime;
                        totalDistance += log.Distance;
                    }



                    stack.Item().Text("Total TotalTime: ", TextStyle.Default.Size(20));
                    stack.Item().Text(totalTotalTime);
                    stack.Item().Text("Total Distance: ", TextStyle.Default.Size(20));
                    stack.Item().Text(totalDistance);


                });


            });


        }
    }
}

