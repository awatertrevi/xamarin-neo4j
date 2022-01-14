//
// QueryEditor.cs
//
// Trevi Awater
// 13-01-2022
//
// © Xamarin.Neo4j
//

using System;
using Xamarin.Forms;

namespace Xamarin.Neo4j.Controls
{
    public class QueryEditor : Editor
    {
        public double MaxHeight { get; set; }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            var sizeRequest = base.OnMeasure(widthConstraint, heightConstraint);

            var newHeight = sizeRequest.Request.Height;

            if (newHeight > MaxHeight)
                newHeight = MaxHeight;

            return new SizeRequest(new Size(sizeRequest.Request.Width, newHeight));
        }
    }
}
