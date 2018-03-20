# Extension Methods
1. Flatten the numbers in `xs`:

   `xs.SelectMany(s => s);`
   
2. Select numbers in `ys` which are divisible by 7 and greater than 42:

   `ys.Select(x => x).Where(x % 7 == 0 && x > 42)`
   
3. Select numbers in `ys` which are leap years.

   `ys.Select(x => x).Where(x % 4 == 0 && ((year % 100 != 0) || (year % 100 == 0 && year % 400 == 0));`
   
# Delegates / Anonymous methods
1. A method which takes a `string` and prints the content in reverse order (by character):

   `Action<string> action = s =>
            {
                foreach (var v in s.Reverse())
                {
                    Console.Write(v);
                }
            };`

2. A method which takes two decimals and returns the product:

   `Func<double, double, double> product = (double a, double b) => a * b;`

3. A method which takes a whole number and a `string` and returns `true` if they are numerically equal. Note that the `string` " 0042" should return `true` if the number is 42:

   `Func<int, string, bool> equivalent = (n, s) =>
            {
                if(int.TryParse(str, out var eq))
                {
                    return eq == n;
                }
                return false;
            };`
