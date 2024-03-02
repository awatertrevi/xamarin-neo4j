//
// QueryEditorRenderer.cs
//
// Trevi Awater
// 13-01-2022
//
// © Xamarin.Neo4j.iOS
//

using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Neo4j.Controls;
using Xamarin.Neo4j.iOS.CustomRenderers;

[assembly: ExportRenderer(typeof(QueryEditor), typeof(QueryEditorRenderer))]
namespace Xamarin.Neo4j.iOS.CustomRenderers
{
    public class QueryEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null && Element != null)
            {
                var executeButton = new UIBarButtonItem("Execute", UIBarButtonItemStyle.Done, (sender, args) =>
                {
                    if (Element is QueryEditor queryEditor)
                    {
                        queryEditor.RaiseExecuteClicked();
                        
                        Control.ResignFirstResponder();
                    }
                });
                
                var toolbar = new UIToolbar(new CGRect(0.0f, 0.0f, Control.Frame.Size.Width, 44.0f));
                toolbar.Items = new[]
                {
                    new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                    executeButton
                };

                Control.InputAccessoryView = toolbar;
            }
        }
    }
}
