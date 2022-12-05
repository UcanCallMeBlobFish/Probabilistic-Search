using System.Buffers;

int[] arrr = {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 100};

static int[] find (int[] a, int x) {
        return find0(a, x, 0, a.Length-1, 1);
}

 static int[] find0 (int[] a, int x, int n1, int n2, int numberOfSteps) {
        int t = (n1+n2)/2;
        
        if (a[t] == x)
                return new int[]{t, numberOfSteps};
        
        else if (n1 >= n2)
                return new int[]{-1, numberOfSteps};
        
        else if (x > a[t])
                return find0 (a,x,t+1,n2, numberOfSteps + 1);
        else if (n1 < t)
                return find0 (a,x,n1,t-1, numberOfSteps + 1);
        else return new int[]{-1, numberOfSteps};
}

static int[] probalisticSearch(int[] arr, int value)
{       
        
        
        Double numerator = Convert.ToDouble((arr[arr.Length - 1] - arr[0])) / Convert.ToDouble(arr.Length - 1);
        Double pos = Convert.ToDouble(value - arr[0])/ numerator;
        int position = Convert.ToInt32(pos);
        return acc(arr, value, position, position, 1);
}

///accumulator function for probabilisticsearch.
static int[] acc(int[] arr, int value, int position, int oldposition, int steps)
{
        if (value == arr[position]) // base case
        {
                return new int[] { position, steps };
        }
        
        else if (arr[position] > value && arr[oldposition] > value ) // left side search, if current and last pos was also bigger than value.
        {
                if ((position - Convert.ToInt32(Math.Pow(2,steps-1))) < 0)  ///If next step is out of bound, left side.
                {
                        return acc(arr, value, 0, position, steps + 1);
                }
                else
                {
                        return acc(arr, value, (position - Convert.ToInt32(Math.Pow(2, steps - 1))), position, steps + 1);
                }
        }
        else if (arr[position] < value && arr[oldposition] > value) // run binary. left side search, if current is less(gadacda), old is smaller
        {
                return find0(arr, value, position + 1, oldposition-1, steps);
        }
        else if (arr[position] < value && arr[oldposition] < value) // continue probabilistic, right search
        {
                if ((position + Convert.ToInt32(Math.Pow(2,steps-1))) > arr.Length-1)
                {
                        return acc(arr, value, arr.Length - 1, position, steps + 1);
                }
                else
                {
                        return acc(arr, value, (position + Convert.ToInt32(Math.Pow(2,steps-1))), position, steps+1);

                }
                
        }
        else if (arr[position] > value && arr[oldposition] < value) // gadacdoma, run binary
        {
                return find0(arr, value, oldposition + 1, position - 1, steps);

        }
        return new[] {-1,steps};
}

/// Compare function
static void compareApproaches(int[] arr, int min, int max)
{
        
        int[] mintomax = new int[Math.Abs(min) + Math.Abs(max)]; // Array to get elements Min to max
        int[] stepcounter = new int[Math.Abs(min) + Math.Abs(max)];
        int[] temp = new int[2];
        for (int i = 0; i < mintomax.Length; i++)
        {
                mintomax[i] = min + i;
        }
        
        probabilisticcounter(arr,mintomax,stepcounter,temp,min,max);
        binarycounter(arr,mintomax,stepcounter,temp,min,max);

}

static void binarycounter(int[] arr, int[] mintomax, int[] stepcounter, int[] temp, int min, int max)
{
        
        for (int i = 0; i < mintomax.Length; i++)
        {
                temp = find(arr, i);
                stepcounter[i] = temp[1];
        }

        Console.WriteLine("Binary result:");
        Console.WriteLine("Max step occured:" + stepcounter.Max()); // print Max step during the call
        Console.WriteLine("Value at which the maximum number of steps occurs:" + mintomax[Array.IndexOf( stepcounter,stepcounter.Max() )]); // print searching value when max step occured.
        Console.WriteLine("total steps: " + stepcounter.Sum());
}

static void probabilisticcounter(int[] arr, int[] mintomax, int[] stepcounter, int[] temp, int min, int max)
{
        for (int i = 0; i < mintomax.Length; i++)
        {
                mintomax[i] = min + i;
        }

        for (int i = 0; i < mintomax.Length; i++)
        {
                temp = probalisticSearch(arr, i);
                stepcounter[i] = temp[1];
        }
        Console.WriteLine("Probabilistic result:");
        Console.WriteLine("Max step occured:" + stepcounter.Max()); // print Max step during the call
        Console.WriteLine("Value at which the maximum number of steps occurs:" + mintomax[Array.IndexOf( stepcounter,stepcounter.Max() )]); // print searching value when max step occured.
        Console.WriteLine("total steps: " + stepcounter.Sum());
}
// compareApproaches(arrr, 0, 100);