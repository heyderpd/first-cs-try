using System;
using System.Collections.Generic;

namespace RadioTJ
{
	internal static class ListManager
	{
		private static List<VoiceItem> Lista;

		private static VoiceItem ActualItem;

		private static bool prioritTime;

		static ListManager()
		{
			ListManager.Lista = new List<VoiceItem>();
			ListManager.ActualItem = new VoiceItem(null, null, false);
			ListManager.prioritTime = true;
		}

		public static void AddItem(string Name, string Dir, bool Priori)
		{
			ListManager.Lista.Add(new VoiceItem(Name, Dir, Priori));
		}

		public static void Clear()
		{
			ListManager.Lista.Clear();
		}

		public static void GeraOrdem()
		{
			Random random = new Random();
			foreach (VoiceItem listum in ListManager.Lista)
			{
				listum.setPosicao(random.Next(1000));
			}
			try
			{
				ListManager.Lista.Sort(new Comparison<VoiceItem>(ListManager.Ordering));
			}
			catch
			{
			}
		}

		public static VoiceItem getActual()
		{
			return ListManager.ActualItem;
		}

		public static VoiceItem getItemBYname(string Name)
		{
			VoiceItem voiceItem;
			List<VoiceItem>.Enumerator enumerator = ListManager.Lista.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					VoiceItem current = enumerator.Current;
					if (current.getName() != Name)
					{
						continue;
					}
					voiceItem = current;
					return voiceItem;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return voiceItem;
		}

		public static List<VoiceItem> getList()
		{
			return ListManager.Lista;
		}

		public static void invertPrioriBYname(string Name)
		{
			foreach (VoiceItem listum in ListManager.Lista)
			{
				if (listum.getName() != Name)
				{
					continue;
				}
				listum.setPriori(!listum.getPriori());
				return;
			}
		}

		public static void itemInactive(VoiceItem thisItem)
		{
			ListManager.itemSetActivit(thisItem, false);
		}

		public static void itemSetActivit(VoiceItem thisItem, bool Activit)
		{
			foreach (VoiceItem listum in ListManager.Lista)
			{
				if (listum != thisItem)
				{
					continue;
				}
				listum.active = Activit;
			}
		}

		public static bool ListOut()
		{
			if (ListManager.Lista.Count > 0)
			{
				return false;
			}
			return true;
		}

		public static int Ordering(VoiceItem a, VoiceItem b)
		{
			if (a == null)
			{
				if (b == null)
				{
					return 0;
				}
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			int posicao = a.getPosicao();
			int num = b.getPosicao();
			if (posicao == num)
			{
				return 0;
			}
			if (posicao > num)
			{
				return 1;
			}
			return -1;
		}

		public static bool prioriGet()
		{
			return ListManager.prioritTime;
		}

		public static bool prioriGetInvert()
		{
			ListManager.prioritTime = !ListManager.prioritTime;
			return ListManager.prioritTime;
		}

		public static void removeItemBYdir(string Dir)
		{
			foreach (VoiceItem listum in ListManager.Lista)
			{
				if (listum.getFileName() != Dir)
				{
					continue;
				}
				ListManager.Lista.Remove(listum);
				return;
			}
		}

		public static void removeItemBYitem(VoiceItem Item)
		{
			ListManager.Lista.Remove(Item);
		}

		public static void removeItemBYname(string Name)
		{
			foreach (VoiceItem listum in ListManager.Lista)
			{
				if (listum.getName() != Name)
				{
					continue;
				}
				ListManager.Lista.Remove(listum);
				return;
			}
		}

		public static void renameBYitem(VoiceItem thisItem, string newName)
		{
			foreach (VoiceItem listum in ListManager.Lista)
			{
				if (listum != thisItem)
				{
					continue;
				}
				listum.setName(newName);
				return;
			}
		}

		public static void setAllActive()
		{
			if (ListManager.ListOut())
			{
				return;
			}
			foreach (VoiceItem listum in ListManager.Lista)
			{
				listum.active = true;
			}
		}

		public static void setNext(VoiceItem Item)
		{
			ListManager.ActualItem = Item;
		}

		public static void setNext()
		{
			ListManager.ActualItem = ListManager.setNextSub(3);
			if (ListManager.ActualItem == null)
			{
				ListManager.ActualItem = new VoiceItem(null, null, false);
			}
		}

		public static VoiceItem setNextSub(int outRecur = 3)
		{
			if (ListManager.ListOut())
			{
				return null;
			}
			ListManager.prioriGetInvert();
			VoiceItem voiceItem = null;
			foreach (VoiceItem listum in ListManager.Lista)
			{
				if (listum.getPriori() != ListManager.prioriGet() || !listum.active)
				{
					continue;
				}
				voiceItem = listum;
			}
			if (voiceItem != null)
			{
				return voiceItem;
			}
			switch (outRecur)
			{
				case 0:
				{
					return null;
				}
				case 1:
				{
					return ListManager.setNextSub(outRecur - 1);
				}
				case 2:
				{
					ListManager.setAllActive();
					return ListManager.setNextSub(outRecur - 1);
				}
				default:
				{
					return ListManager.setNextSub(outRecur - 1);
				}
			}
		}
	}
}