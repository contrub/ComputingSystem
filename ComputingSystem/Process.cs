using System.Diagnostics.Contracts;

namespace ComputingSystem
{
    enum ProcessStatus
    {
        ready,
        runnning,
        waiting,
        terminated,
        running
    }

    class Process : IComparable<Process>
    {
        public Process(long pId, long addrSpace)
        {
            id = pId;
            AddrSpace = addrSpace;
            name = "P" + pId.ToString();
            Status = ProcessStatus.ready;
        }

        public void IncreaseWorkTime()
        {
            if (workTime < BurstTime)
            {
                workTime++;
                return;
            }

            OnFreeingAResource();
        }

        public void ResetWorkTime()
        {
            workTime = 0;
        }

        [Pure]
        public override string ToString()
        {
            return "Proc" + id + "; BurstTime: " + BurstTime + "; WorkTime: " + workTime.ToString() +
                "; Status: " + Status.ToString();
        }

        public int CompareTo(Process otherProc)
        {
            if (otherProc == null)
            {
                return 1;
            }

            return otherProc.BurstTime.CompareTo(BurstTime);
        }

        public event EventHandler FreeingAResource;

        private void OnFreeingAResource()
        {
            FreeingAResource?.Invoke(this, null);
        }


        private readonly Random rnd = new();

        private readonly long id;

        private readonly string name;

        public long BurstTime { get; set; }

        public ProcessStatus Status { get; set; }

        private long workTime;

        public long AddrSpace { get; private set; }
    }
}