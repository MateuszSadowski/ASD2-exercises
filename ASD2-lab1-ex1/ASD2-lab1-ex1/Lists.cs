
using System.Collections.Generic;

namespace ASD
{

public interface IList : IEnumerable<int>
    {
    // Jeœli element v jest na liœcie to zwraca true
    // Jeœli elementu v nie ma na liœcie to zwraca false
    bool Search(int v);

    // Jeœli element v jest na liœcie to zwraca false (elementu nie dodaje)
    // Jeœli elementu v nie ma na liœcie to dodaje go do listy i zwraca true
    bool Add(int v);

    // Jeœli element v jest na liœcie to usuwa go z listy i zwraca true
    // Jeœli elementu v nie ma na liœcie to zwraca false
    bool Remove(int v);
    }

//
// dopisaæ klasê opisuj¹c¹ element listy
//
    public class Node
    {
        public int val;
        public Node next;

        public Node(int _val, Node _next)
        {
            val = _val;
            next = _next;
        }
    }

// Zwyk³a lista (nie samoorganizuj¹ca siê)
public class SimpleList : IList
    {

        // dodaæ niezbêdne pola
        public Node head;
        public Node tail;

        public SimpleList()
        {
            head = null;
            tail = null;
        }

    // Lista siê nie zmienia
    public bool Search(int v)
        {
            if (null == head)
                return false;

            Node node = head;
            do
            {
                if (node.val == v)
                    return true;
                node = node.next;
            } while (null != node);

        return false;  // zmieniæ
        }

    // Element jest dodawany na koniec listy
    public bool Add(int v)
        {
            if (Search(v))
                return false;
            
            if(null == head)
            {
                head = tail = new Node(v, null);
                return true;
            }

            tail.next = new Node(v, null);
            tail = tail.next;

        return true;  // zmieniæ
        }

    // Pozosta³e elementy nie zmieniaj¹ kolejnoœci
        //TODO: fix this
    public bool Remove(int v)
        {
            if (!Search(v))
                return false;
            
            Node node = head;
            Node prev = null;
            do
            {
                if (node.val == v)
                {
                    if (head == tail)   //Only element
                    {
                        head = tail = null;
                        return true;
                    }
                    else if(null == prev)   //First element
                    {
                        head = node.next;
                    }
                    else if(null == node.next)  //Last element
                    {
                        tail = prev;
                        prev.next = null;
                    }
                    else
                    {
                        prev.next = node.next;
                    }

                    return true;
                }

                prev = node;
                node = node.next;
            } while (null != node);
                
        return false;  // zmieniæ
        }

    // Wymagane przez interfejs IEnumerable<int>
    public IEnumerator<int> GetEnumerator()
        {
        // nie wolno modyfikowaæ kolekcji
            Node node = head;
            while (null != node)
            {
                yield return node.val;
                node = node.next;
            }
            //yield break;
        }

    // Wymagane przez interfejs IEnumerable<int> - nie zmmieniaæ (jest gotowe!)
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
        return this.GetEnumerator();
        }

    } // class SimpleList


// Lista z przesnoszeniem elementu, do którego by³ dostêp na pocz¹tek
public class MoveToFrontList : IList
    {

    // dodaæ niezbêdne pola
        public Node head;
        //public Node tail;

        public MoveToFrontList()
        {
            head = null;
            //tail = null;
        }

    // Znaleziony element jest przenoszony na pocz¹tek
    public bool Search(int v)
        {
            if (null == head)
                return false;

            Node node = head;
            Node prev = null;
            do
            {
                if (node.val == v)
                {
                    if (null == prev)
                        return true;
                    else
                    {
                        prev.next = node.next;
                        node.next = head;
                        head = node;
                    }
                    return true;   
                }
                prev = node;
                node = node.next;
            } while (null != node);

        return false;  // zmieniæ
        }

    // Element jest dodawany na pocz¹tku, a jeœli ju¿ by³ na liœcie to jest przenoszony na pocz¹tek
    public bool Add(int v)
        {
            if (Search(v))
                return false;
            Node node = new Node(v, head);
            head = node;
        return true;  // zmieniæ
        }

    // Pozosta³e elementy nie zmieniaj¹ kolejnoœci
    public bool Remove(int v)
        {
            Node node = head;
            Node prev = null;
            while (null != node)
            {
                if (node.val == v)
                {
                    if (null == prev)
                        head = node.next;   //FREE HEAD?
                                            //else if (null == node.next)
                                            //prev.next = null;   //FREE NODE?
                    else
                        prev.next = node.next;

                    return true;
                }

                prev = node;
                node = node.next;
            }

            return false;  // zmieniæ
        }

    // Wymagane przez interfejs IEnumerable<int>
    public IEnumerator<int> GetEnumerator()
        {
        // nie wolno modyfikowaæ kolekcji
            Node node = head;
            while (null != node)
            {
                yield return node.val;
                node = node.next;
            }
        }

    // Wymagane przez interfejs IEnumerable<int> - nie zmmieniaæ (jest gotowe!)
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
        return this.GetEnumerator();
        }

    } // class MoveToFrontList


} // namespace ASD
