using System;
using System.Text;

namespace hashes
{
	public class GhostsTask : 
		IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>, 
		IMagic
	{
		public void DoMagic()
		{
		}

		// Чтобы класс одновременно реализовывал интерфейсы IFactory<A> и IFactory<B> 
		// придется воспользоваться так называемой явной реализацией интерфейса.
		// Чтобы отличать методы создания A и B у каждого метода Create нужно явно указать, к какому интерфейсу он относится.
		// На самом деле такое вы уже видели, когда реализовывали IEnumerable<T>.

		Vector IFactory<Vector>.Create()
		{
			throw new NotImplementedException();
		}

		Segment IFactory<Segment>.Create()
		{
			throw new NotImplementedException();
		}
        Document IFactory<Document>.Create()
        {
            throw new NotImplementedException();
        }

        Cat IFactory<Cat>.Create()
        {
            throw new NotImplementedException();
        }

        Robot IFactory<Robot>.Create()
        {
            throw new NotImplementedException();
        }
    }
}