# CronaDataParser
Small app to read data about Corona

Data is loaded from: https://ourworldindata.org/coronavirus-data
Run console application and data is exported using csv (with ';' as delimiter) for countries selected in program.cs

Data exported in the format of number of new positive for covid-19 per 100 000 people per week.  It is calculated on average of selected dataset and multiplied by 7 (1 week).  

20 is the current limit set as acceptable by Norwegian authorities for returning to Norway without a quarantine.

https://www.fhi.no/en/news/2020/grenseapning-i-forbindelse-med-covid-19/

Even though the articles states 2 weeks, the actual calculations take average of the two last weeks, making the actual values weekly numbers and not by-weekly:
https://www.fhi.no/contentassets/0c17dede311e4e8f823a0e021d1f3ad0/2020.07.10-tallgrunnlag-norden-og-europa.xlsx

