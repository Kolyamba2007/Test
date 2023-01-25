class ListRand
{
    public ListNode Head;
    public ListNode Tail;
    public int Count;

    public void Serialize(FileStream s)
    {
        BinaryWriter writer = new BinaryWriter(s);
        ListNode current = Head;

        writer.Write(Count);

        while (current != null)
        {
            writer.Write(current.Data);

            if (current.Rand != null)
            {
                int index = GetNodeIndex(current.Rand);
                writer.Write(index);
            }
            else
                writer.Write(-1);

            current = current.Next;
        }
    }

    public void Deserialize(FileStream s)
    {
        BinaryReader reader = new BinaryReader(s);
        List<ListNode> nodes = new List<ListNode>();

        Count = reader.ReadInt32();

        for (int i = 0; i < Count; i++)
            nodes.Add(new ListNode());

        for (int i = 0; i < Count; i++)
        {
            string data = reader.ReadString();
            nodes[i].Data = data;

            int index = reader.ReadInt32();

            if (index != -1)
                nodes[i].Rand = nodes[index];
            if (i > 0)
                nodes[i - 1].Next = nodes[i];
            if (i < Count - 1)
                nodes[i + 1].Prev = nodes[i];
        }

        Head = nodes[0];
        Tail = nodes[Count - 1];
    }

    public void FillListRand(int count)
    {
        for (int i = 0; i < count; i++)
        {
            ListNode node = new ListNode();
            node.Data = i.ToString();

            if (Head == null)
            {
                Head = node;
                Tail = node;
            }
            else
            {
                Tail.Next = node;
                node.Prev = Tail;
                Tail = node;
            }

            Count++;
        }

        Random rand = new Random();
        ListNode current = Head;

        while (current != null)
        {
            int randIndex = rand.Next(0, Count);
            ListNode randNode = Head;

            for (int i = 0; i < randIndex; i++)
                randNode = randNode.Next;

            current.Rand = randNode;
            current = current.Next;
        }
    }

    private int GetNodeIndex(ListNode node)
    {
        ListNode current = Head;
        int index = 0;

        while (current != null)
        {
            if (current == node)
                return index;

            current = current.Next;
            index++;
        }

        return -1;
    }
}