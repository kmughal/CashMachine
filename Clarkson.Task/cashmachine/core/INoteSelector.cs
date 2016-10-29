namespace Clarkson.Task
{
    using System.Collections.Generic;

    public interface INoteSelector
    {
        int GetNote(int amount, Dictionary<int, int> availableNotes);
    }
}
