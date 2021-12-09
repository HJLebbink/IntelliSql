
namespace AsyncCompletionSample
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.Shell;

    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [ProvideService(typeof(IntelliSqlPackage), IsAsyncQueryable = true)]

    public sealed class IntelliSqlPackage : AsyncPackage
    {
        public const string PackageGuidString = "27e0e7ef-ecaf-4b87-a574-6a909383f911";
        internal const string ThisContentType = "sql!";

        public IntelliSqlPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "=========================================\nINFO: AsyncCompletionSample: Entering constructor"));
        }

        protected override Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            //this.AddService(typeof(SMyTestService), CreateService, true);
            return Task.FromResult<object>(null);
        }
    }
}
