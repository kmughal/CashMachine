namespace Clarkson.Task
{
    using System.Collections.Generic;

    public class NoteSelectorAlogrithm1 : INoteSelector
    {
        public int GetNote(int amount, Dictionary<int, int> availableNotes)
        {
            foreach (var kvp in availableNotes)
            {
                if (amount >= kvp.Key && kvp.Value > 0)
                    return kvp.Key;
            }

           throw ExceptionHelpers.ThrowNoNotesException();          
        }
    }
}
