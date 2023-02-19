using System;

namespace Diabloid
{
    [Serializable]
    public class GoldData
    {
        public int Amount;
        public Action Changed;

        public void Collect(int value)
        {
            Amount += value;
            Changed?.Invoke();
        }
    }
}