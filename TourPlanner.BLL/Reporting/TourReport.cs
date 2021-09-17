using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using TourPlanner.Models;

namespace TourPlanner.BLL.Reporting
{
    class TourReport : IDocument
    {
        private TourItem _tour;
        private List<LogItem> _tourLogs;
        private Byte[] _image;

        public TourReport(TourItem currentTour, List<LogItem> tourLogs)
        {
            this._tour = currentTour;
            this._image = File.ReadAllBytes(currentTour.ImagePath);
            this._tourLogs = tourLogs;
        }

        public DocumentMetadata GetMetadata()
        {
            return new DocumentMetadata() {Title = _tour.Name + "_TourReport"};
        }

        public void Compose(IContainer container)
        {
            container.Row(row =>
            {
                
                


                row.RelativeColumn().Stack(stack =>
                {
                    if (_image is not null)
                        //stack.Item().Image(_image, ImageScaling.FitArea);
                        stack.Item().Height(250).Width(250).Image(_image);
                    stack.Item().Text(_tour.Name, TextStyle.Default.Size(20));
                    stack.Item().Text("From: " + _tour.From);
                    stack.Item().Text("To: " + _tour.To);
                    stack.Item().Text("Distance: " + _tour.Distance);
                    stack.Item().Text("Description: " + _tour.Description);
                    stack.Item().Text("Route Information: " + _tour.RouteInformation);



                    foreach (var log in _tourLogs)
                    {
                        stack.Item().Padding(5).Row(row =>
                        {
                            row.RelativeColumn().AlignRight().Text(log.DateTime.ToShortDateString());
                            row.RelativeColumn().AlignRight().Text(log.TotalTime);
                            row.RelativeColumn().AlignRight().Text(log.Report);
                            row.RelativeColumn().AlignRight().Text(log.Distance);
                            row.RelativeColumn().AlignRight().Text(log.Rating);
                            row.RelativeColumn().AlignRight().Text(log.AverageSpeed);
                            row.RelativeColumn().AlignRight().Text(log.MaxSpeed);
                            row.RelativeColumn().AlignRight().Text(log.MinSpeed);
                            row.RelativeColumn().AlignRight().Text(log.AverageStepCount);
                            row.RelativeColumn().AlignRight().Text(log.BurntCalories);
                        });
                    }
                });

                
            });


        }
    }
}
