using Microsoft.VisualStudio.Language.Intellisense.AsyncCompletion;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace AsyncCompletionSample.JsonElementCompletion
{
    [Export(typeof(IAsyncCompletionCommitManagerProvider))]
    [Name("SampleCompletionCommitManagerProvider")]
    [ContentType(IntelliSqlPackage.ThisContentType)]
    class SampleCompletionCommitManagerProvider : IAsyncCompletionCommitManagerProvider
    {
        private IDictionary<ITextView, IAsyncCompletionCommitManager> cache = new Dictionary<ITextView, IAsyncCompletionCommitManager>();

        public IAsyncCompletionCommitManager GetOrCreate(ITextView textView)
        {
            if (this.cache.TryGetValue(textView, out var itemSource))
            {
                return itemSource;
            }
            var manager = new SampleCompletionCommitManager();
            textView.Closed += (o, e) => this.cache.Remove(textView); // clean up memory as files are closed
            this.cache.Add(textView, manager);
            return manager;
        }
    }
}
