﻿using ExpenseMobileApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ExpenseMobileApp
{
    public static class ExpenseManager
    {

        private static List<YearlyExpense> yearlyExpenseList = new List<YearlyExpense>();
        public static void InitializeData()
        {
            string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "YearlyExpenses.xml");
            bool isfilethere = File.Exists(filepath);
            if (isfilethere)
            {

                //File.Delete(filepath);
                DeSerializeData();
            }
        }

        public static bool IsMonthInitialised(int year, int month)
        {
            bool retval = false;
            //initialise data from collection
            foreach(YearlyExpense item in yearlyExpenseList)
            {
                //check if the year matches first
                if(item.Year == year)
                {
                    //check further if the month is there
                    foreach (MonthlyExpense exp in item.monthlyExpenseList)
                    {
                        if (exp.Month == month) { retval = true; }
                    }
                }
            }
            
            return retval;
        }

        public static void InitializeMonthlyBudget(int budget, int year, int month)
        {
            bool foundYear = false;
            //create new month
            MonthlyExpense newmonth = new MonthlyExpense { Budget = budget, ExpenseList = new List<Expense>(), Month = month };
            
            foreach (YearlyExpense item in yearlyExpenseList)
            {
                //check if the year matches first
                if (item.Year == year)
                {
                    //year is already there , just initialise for the month and go ahead
                    
                    item.monthlyExpenseList.Add(newmonth);
                    foundYear = true;
                    break;
                    
                }
            }
            if (!foundYear)
            {
                //create a year object and add the new month to it
                YearlyExpense newyear = new YearlyExpense { Year = year, monthlyExpenseList = new List<MonthlyExpense>() };
                newyear.monthlyExpenseList.Add(newmonth);
                yearlyExpenseList.Add(newyear);

            }
            //data changed - update the data source
            SerializeData();
           

        }
        //this method will fetch the list of expenses for a particular month

        public static void GetMonthlyExpenses(int month, int year, ref MonthlyExpense monthlyexpense)
        {
            foreach (YearlyExpense item in yearlyExpenseList)
            {
                //check if the year matches first
                if (item.Year == year)
                {
                    //year is already there , just initialise for the month and go ahead
                    var expenselist = item.monthlyExpenseList.Where(eachmonth => eachmonth.Month == month).ToList();
                    //this will have only one item
                    monthlyexpense = expenselist[0];
                    break;

                }
            }

            
        }

        public static void AddMonthlyExpense(int month, int year, Expense expenses)
        {
            foreach (YearlyExpense item in yearlyExpenseList)
            {
                //check if the year matches first
                if (item.Year == year)
                {
                    //whenever we add expense - let us make sure to write everything to a file
                    var toadd = item.monthlyExpenseList.Where(eachmonth => eachmonth.Month == month).ToList();
                    toadd[0].ExpenseList.Add(expenses);
                    break;
                }
            }
            SerializeData();
        }

        public static void DeleteMonthlyExpense(int month, int year, Expense expenses)
        {
            foreach (YearlyExpense item in yearlyExpenseList)
            {
                if (item.Year == year)
                {
                    var toremove = item.monthlyExpenseList.Where(eachmonth => eachmonth.Month == month).ToList();
                    toremove[0].ExpenseList.Remove(expenses);
                    break;
                }
            }
            SerializeData();
        }

        private static void DeSerializeData()
        {
            string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "YearlyExpenses.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(List<YearlyExpense>));//initialises the serialiser
                                                                                      //this should read stuff from the xml and update the static collection with the data
                                                                                      //deserialize the data already stored
            Stream reader = new FileStream(filepath, FileMode.Open);//initialises the writer

            yearlyExpenseList = (List<YearlyExpense>)serializer.Deserialize(reader);//Writes to the file
            reader.Close();
        }

        private static void SerializeData()
        {
            //serialize this data for the app to remember
            XmlSerializer serializer = new XmlSerializer(typeof(List<YearlyExpense>));//initialises the serialiser
            string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "YearlyExpenses.xml");
            Stream writer = new FileStream(filepath, FileMode.Create);//initialises the writer

            serializer.Serialize(writer, yearlyExpenseList);//Writes to the file
            writer.Close();
        }

    }


}


