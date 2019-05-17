using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParrotWingsTests.Helpers
{
    public class AsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> inner;

        public AsyncEnumerator(IEnumerator<T> inner)
        {
            this.inner = inner;
        }

        public void Dispose()
        {
            inner.Dispose();
        }

        public T Current => inner.Current;

        public Task<bool> MoveNext(CancellationToken cancellationToken) => Task.FromResult(inner.MoveNext());
    }
}