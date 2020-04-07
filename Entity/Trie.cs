using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.Entity
{
    public class Trie
    {
        static readonly int ALPHABETS = 26;

        class TrieNode
        {
            public TrieNode[] trieNodes;

            public bool isCompleteString;

            public object data;

            public TrieNode()
            {
                trieNodes = new TrieNode[ALPHABETS];
                isCompleteString = false;
                data = null;
            }
        }

        private TrieNode root;

        public Trie()
        {
            root = new TrieNode();
        }

        public bool insert(String str, object data)
        {
            TrieNode trieNode = null;

            str = str.ToLower();

            if ((trieNode = search(str)) != null && trieNode.isCompleteString)
                return false;

            TrieNode traverser = root;

            for (int strIndex = 0; strIndex < str.Length; strIndex++)
            {
                int insertionIndex = str[strIndex] - 'a';
                if (traverser.trieNodes[insertionIndex] == null)
                    traverser.trieNodes[insertionIndex] = new TrieNode();
                traverser = traverser.trieNodes[insertionIndex];
            }

            traverser.isCompleteString = true;
            traverser.data = data;

            return true;
        }

        private TrieNode search(String key)
        {
            key = key.ToLower();

            TrieNode traverser = root;

            for (int keyIndex = 0; keyIndex < key.Length; keyIndex++)
            {
                int nextSearchIndex = key[keyIndex] - 'a';
                if (traverser.trieNodes[nextSearchIndex] == null)
                    return null;

                traverser = traverser.trieNodes[nextSearchIndex];
            }

            return traverser != null ? traverser : null;
        }

        public List<KeyValuePair<string, object>> prefixSearch(String key)
        {
            TrieNode traverser = root;

            TrieNode matchingRoot = getTrieNodeByKey(key);

            return matchingRoot == null ? null : getCompletedStartingFrom(matchingRoot, key);
        }

        private TrieNode getTrieNodeByKey(String key)
        {
            key = key.ToLower();

            TrieNode traverser = root;

            for (int keyIndex = 0; keyIndex < key.Length; keyIndex++)
            {
                int nextSearchIndex = key[keyIndex] - 'a';
                if (traverser.trieNodes[nextSearchIndex] == null)
                    return null;

                traverser = traverser.trieNodes[nextSearchIndex];
            }

            return traverser != null ? traverser : null;
        }

        private List<KeyValuePair<string, object>> getCompletedStartingFrom(TrieNode trieNode, String key)
        {
            List<KeyValuePair<string, object>> completed = null;

            if (trieNode.isCompleteString)
            {
                completed = new List<KeyValuePair<string, object>>();
                completed.Add(new KeyValuePair<string, object>(key, trieNode.data));
            }

            List<KeyValuePair<string, object>> furtherCompleted = new List<KeyValuePair<string, object>>();

            for (int alphabetIndex = 0; alphabetIndex < ALPHABETS; alphabetIndex++)
            {
                if (trieNode.trieNodes[alphabetIndex] != null)
                {
                    List<KeyValuePair<string, object>> completedStartingFromCurrent =
                        getCompletedStartingFrom(
                            trieNode.trieNodes[alphabetIndex]
                            , new StringBuilder(key).Append(Convert.ToChar('a' + alphabetIndex)).ToString());

                    if (completedStartingFromCurrent != null)
                    {
                        furtherCompleted.AddRange(completedStartingFromCurrent);
                    }
                }
            }

            if (furtherCompleted != null && furtherCompleted.Count != 0)
            {
                if (completed == null)
                    completed = new List<KeyValuePair<string, object>>();
                completed.AddRange(furtherCompleted);
            }

            return completed;
        }

        public bool delete(String key)
        {
            key = key.ToLower();

            TrieNode toBeDeleted = null;
            if ((toBeDeleted = search(key)) == null)
                return false;

            toBeDeleted.data = null;
            toBeDeleted.isCompleteString = false;

            if (toBeDeleted.trieNodes.ToList().All(trieNode => trieNode == null))
            {
                TrieNode parentTrieNode = search(key.Remove(key.Length - 1));
                int indexToClear = key[key.Length - 1] - 'a';
                parentTrieNode.trieNodes[indexToClear] = null;
            }

            return true;
        }
    }
}
