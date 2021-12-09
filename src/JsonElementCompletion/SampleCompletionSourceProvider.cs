using Microsoft.VisualStudio.Language.Intellisense.AsyncCompletion;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Utilities;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace AsyncCompletionSample.JsonElementCompletion
{
    [Export(typeof(IAsyncCompletionSourceProvider))]
    [Name("Chemical element dictionary completion provider")]
    [ContentType(IntelliSqlPackage.ThisContentType)]
    class SampleCompletionSourceProvider : IAsyncCompletionSourceProvider
    {
        IDictionary<ITextView, IAsyncCompletionSource> cache = new Dictionary<ITextView, IAsyncCompletionSource>();

        [Import]
        ElementCatalog Catalog;

        [Import]
        ITextStructureNavigatorSelectorService StructureNavigatorSelector;

        public IAsyncCompletionSource GetOrCreate(ITextView textView)
        {
            if (this.cache.TryGetValue(textView, out var itemSource))
                return itemSource;

            var source = new SampleCompletionSource(this.Catalog, this.StructureNavigatorSelector); // opportunity to pass in MEF parts
            textView.Closed += (o, e) => this.cache.Remove(textView); // clean up memory as files are closed
            this.cache.Add(textView, source);
            return source;
        }
    }
}
