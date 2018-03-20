using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BDSA2017.Assignment03
{
    public class Queries
    {
        //Wizards invented by Rowling after 2002. Only return the names.
        public static IEnumerable<string> RowlingWizardsAfter2002Extension(IEnumerable<Wizard> wizards)
        {
            return wizards.Where(wizard => wizard.Author == "J. K. Rowling" && wizard.Year > 2002)
                          .Select(wizard => wizard.Name);
        }

        public static IEnumerable<string> RowlingWizardsAfter2002Linq(IEnumerable<Wizard> wizards)
        {
            return from wizard in wizards
                where wizard.Author == "J. K. Rowling" && wizard.Year > 2002
                select wizard.Name;
        }


        //The year the first sith lord was introduced. Let's define a sith lord to be one named Darth something from a medium containing the words Star Wars

        public static int YearOfFirthSithExtension(IEnumerable<Wizard> wizards)
        {
            return wizards.Where(wizard => wizard.Year != null && wizard.Medium == "Star Wars" && wizard.Name.StartsWith("Darth"))
                          .Select(wizard => wizard.Year)
                          .Min() ?? -1;
        }

        public static int YearOfFirthSithLinq(IEnumerable<Wizard> wizards)
        {
            return (
                from wizard in wizards
                where wizard.Year != null && wizard.Medium == "Star Wars" && wizard.Name.StartsWith("Darth")
                select wizard.Year)
                .Min() ?? -1;
        }


        //Unique list of Harry Potter books - only return medium and year (as a value tuple)

        public static IEnumerable<(string Medium, int? Year)> ListOfHarryPotterBooksExtension(IEnumerable<Wizard> wizards)
        {
            return (wizards.Where(wizard => wizard.Medium.Contains("Harry Potter"))
                .Select(wizard => (wizard.Medium, wizard.Year))).Distinct();
        }

        public static IEnumerable<(string Medium, int? Year)> ListOfHarryPotterBooksLinq(IEnumerable<Wizard> wizards)
        {
            return (from wizard in wizards
                where wizard.Medium.Contains("Harry Potter")
                select (wizard.Medium,wizard.Year)).Distinct();
        }


        //List of wizard names grouped by author in reverse order by author name.

        public static IEnumerable<string> ListOfWizardNamesReverseOrderByAuthorExtension(IEnumerable<Wizard> wizards)
        {
            return wizards.OrderByDescending(wizard => wizard.Author).Select(wizard => wizard.Name);
        }

        public static IEnumerable<string> ListOfWizardNamesReverseOrderByAuthorLinq(IEnumerable<Wizard> wizards)
        {
            return from wizard in wizards
                orderby wizard.Author descending
                select wizard.Name;
        }

    }
}
