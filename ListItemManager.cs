using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace $safeprojectname$
{
    static class ListItemManager
    {
        private static List<VoiceItem> Lista = new List<VoiceItem>();
        private static VoiceItem Next = null;

        public static void Clear()
        {
            Lista.Clear();
            Next = null;
        }

        public static void Add(VoiceItem Item)
        {
            Lista.Add(Item);
        }

        public static void Add(String name, String dir)
        {
            VoiceItem Item = new VoiceItem(name, dir);
            Lista.Add(Item);
        }

        public static void Del(VoiceItem Item)
        {
            Lista.Remove(Item);
        }

        public static void Del(String Name)
        {
            Lista.Remove(Get(Name));
        }

        public static void DelByDir(String Dir)
        {
            foreach (VoiceItem Item in Lista)
            {
                if (Item.getFileName() == Dir)
                {
                    Lista.Remove(Item);
                    break;
                }
            }
            return;
        }

        public static VoiceItem Get(String name)
        {
            foreach (VoiceItem Item in Lista)
            {
                if (Item.getName() == name)
                    return  Item;
            }
            return null;
        }

        public static void GeraOrdem()
        {
            Random R = new Random();
            foreach (VoiceItem Item in Lista)
            {
                Item.setPosicao(R.Next(1000));
            }
            Lista.OrderBy(x => x.getPosicao());
            goToFirst();

            Data.NextName = Next.getName(); // to see in the form
        }

        public static void goToFirst()
        {
            foreach (VoiceItem Item in Lista)
            {
                Next = Item;    // take the first
                break;
            }
        }

        public static VoiceItem goToNext()
        {
            bool thisNext = false;
            bool noNewNext = true;
            if (Next != null)
            {
                foreach (VoiceItem Item in Lista)
                {
                    if(thisNext)
                    {
                        noNewNext = false;
                        Next = Item;
                        break;
                    }
                    if (Item.getName() == Next.getName())
                    {
                        thisNext = true;
                    }
                }
            }
            if (noNewNext)
                goToFirst();

            Data.NextName = Next.getName(); // to see in the form
            return Next;
        }

        public static void setNext(String Name)
        {
            Next = Get(Name);

            Data.NextName = Next.getName(); // to see in the form
        }

        public static VoiceItem getNext()
        {
            return Next;
        }

        public static List<VoiceItem> getList(bool reOrder = false)
        {
            if (reOrder)
                GeraOrdem();
            return Lista;
        }

        public static String[] getStringList(bool reOrder = false)
        {
            List<VoiceItem> List = getList(reOrder);
            List<String> StringList = new List<String>();
            foreach (VoiceItem item in List)
            {
                StringList.Add(item.getName());
            }
            return StringList.ToArray();
        }

        public static void rename(String Name, String newName)
        {
            if (newName.Length > 0 && Get(newName) == null)
            {
                VoiceItem Item = Get(Name);
                Item.setName(newName);
            }
        }
    }
}
