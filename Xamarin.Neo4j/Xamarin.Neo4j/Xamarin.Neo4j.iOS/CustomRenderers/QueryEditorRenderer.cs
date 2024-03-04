//
// QueryEditorRenderer.cs
//
// Trevi Awater
// 13-01-2022
//
// © Xamarin.Neo4j.iOS
//

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CoreGraphics;
using Foundation;
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
        private readonly string[] _keyWords = 
        {
            // Clauses
            "CALL", "CREATE", "DELETE", "DETACH", "FOREACH", "LOAD", "MATCH", "MERGE", "OPTIONAL", "REMOVE", "RETURN", "SET", "START", "UNION", "UNWIND", "WITH",
            
            // Subclauses
            "LIMIT", "ORDER", "SKIP", "WHERE", "YIELD",
            
            // Modifiers
            "ASC", "ASCENDING", "ASSERT", "BY", "CSV", "DESC", "DESCENDING", "ON",
            
            // Expressions
            "ALL", "CASE", "COUNT", "ELSE", "END", "EXISTS", "THEN", "WHEN",
            
            // Operators
            "AND", "AS", "CONTAINS", "DISTINCT", "ENDS", "IN", "IS", "NOT", "OR", "STARTS", "XOR",
            
            // Schema
            "CONSTRAINT", "CREATE", "DROP", "EXISTS", "INDEX", "NODE", "KEY", "UNIQUE",
            
            // Hints
            "INDEX", "JOIN", "SCAN", "USING",
            
            // Literals
            "FALSE", "NULL", "TRUE"
        };
        
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            
            if (e.NewElement != null)
            {
                Element.TextChanged += (sender, args) =>
                {
                    HighlightWords(_keyWords);
                };
            }

            if (Control != null && Element != null)
            {
                Control.AutocorrectionType = UITextAutocorrectionType.No;
                Control.AutocapitalizationType = UITextAutocapitalizationType.None;
                Control.SpellCheckingType = UITextSpellCheckingType.No;
                Control.KeyboardType = UIKeyboardType.Default;

                var executeButton = new UIBarButtonItem("Execute", UIBarButtonItemStyle.Done, (sender, args) =>
                {
                    if (Element is QueryEditor queryEditor)
                    {
                        queryEditor.RaiseExecuteClicked();
                        
                        Control.ResignFirstResponder();
                    }
                });
                
                var keyButtons = new[]
                {
                    new UIBarButtonItem("(", UIBarButtonItemStyle.Plain, (sender, args) => InsertText("(")),
                    new UIBarButtonItem(")", UIBarButtonItemStyle.Plain, (sender, args) => InsertText(")")),
                    new UIBarButtonItem("[", UIBarButtonItemStyle.Plain, (sender, args) => InsertText("[")),
                    new UIBarButtonItem("]", UIBarButtonItemStyle.Plain, (sender, args) => InsertText("]")),
                    new UIBarButtonItem(":", UIBarButtonItemStyle.Plain, (sender, args) => InsertText(":")),
                    new UIBarButtonItem("-", UIBarButtonItemStyle.Plain, (sender, args) => InsertText("-")),
                    new UIBarButtonItem("\u2192", UIBarButtonItemStyle.Plain, (sender, args) => InsertText("->")),
                    new UIBarButtonItem("\u2190", UIBarButtonItemStyle.Plain, (sender, args) => InsertText("<-")),
                    
                    new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                    
                    executeButton
                };
                
                var toolbar = new UIToolbar(new CGRect(0.0f, 0.0f, Control.Frame.Size.Width, 44.0f));
                toolbar.Items = keyButtons;

                Control.InputAccessoryView = toolbar;
                
                HighlightWords(_keyWords);
            }
        }

        private void HighlightWords(IEnumerable<string> wordsToHighlight)
        {
            var text = PreprocessText(Control.Text);
            var attributedText = new NSMutableAttributedString(text);

            // Iterate through the array and apply formatting to the editor's text.
            foreach (var word in wordsToHighlight)
            {
                var regex = new Regex("\\b" + Regex.Escape(word) + "\\b", RegexOptions.IgnoreCase);

                foreach (Match match in regex.Matches(text))
                {
                    attributedText.AddAttribute(UIStringAttributeKey.ForegroundColor, Color.FromHex("#89982e").ToUIColor(), 
                        new NSRange(match.Index, match.Length));
                }
            }
            
            // Apply text color formatting for text inside single quotes.
            ApplyQuoteTextColorFormatting(text, attributedText, "'(.*?)'", Color.FromHex("#ae8b2d").ToUIColor());
    
            // Apply text color formatting for text inside double quotes.
            ApplyQuoteTextColorFormatting(text, attributedText, "\"(.*?)\"", Color.FromHex("#ae8b2d").ToUIColor());

            attributedText.AddAttribute(UIStringAttributeKey.Font, UIFont.FromName("Roboto Mono", (nfloat) Element.FontSize), new NSRange(0, attributedText.Length));

            Control.AttributedText = attributedText;
        }

        private void InsertText(string text)
        {
           Control.InsertText(text);
        }

        private static void ApplyQuoteTextColorFormatting(string text, NSMutableAttributedString attributedText,
            string quotePattern, UIColor color)
        {
            var quoteRegex = new Regex(quotePattern);

            foreach (Match match in quoteRegex.Matches(text))
            {
                attributedText.AddAttribute(UIStringAttributeKey.ForegroundColor, color,
                    new NSRange(match.Index, match.Length));
            }
        }
        
        private static string PreprocessText(string text)
        {
            // Replace single quotation marks and their curly variants with: '
            text = text.Replace("‘", "'");
            text = text.Replace("’", "'");

            // Replace double quotation marks and their curly variants with: "
            text = text.Replace("“", "\"");
            text = text.Replace("”", "\"");

            return text;
        }
    }
}
