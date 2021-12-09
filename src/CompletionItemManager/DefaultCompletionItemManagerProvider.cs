using Microsoft.VisualStudio.Language.Intellisense.AsyncCompletion;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.PatternMatching;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace AsyncCompletionSample.CompletionItemManager
{
    [Export(typeof(IAsyncCompletionItemManagerProvider))]
    [Name("Default completion item manager")]
    [ContentType(IntelliSqlPackage.ThisContentType)]
    [Order(Before = PredefinedCompletionNames.DefaultCompletionItemManager)] // override the default item manager so that we can step through this code
    internal sealed class DefaultCompletionItemManagerProvider : IAsyncCompletionItemManagerProvider
    {
        [Import]
        public IPatternMatcherFactory PatternMatcherFactory;

        private DefaultCompletionItemManager _instance;

        IAsyncCompletionItemManager IAsyncCompletionItemManagerProvider.GetOrCreate(ITextView textView)
        {
            if (this._instance == null)
            {
                this._instance = new DefaultCompletionItemManager(this.PatternMatcherFactory);
            }
            return this._instance;
        }
    }
}
