# CronaDataParser

![.NET Core](https://github.com/Glattetre/CronaDataParser/workflows/.NET%20Core/badge.svg)

Small apps to read data about Corona

Data is loaded from: https://ourworldindata.org/coronavirus-data

Data exported in the format of number of new positive for covid-19 per 100 000 people per week.  It is calculated on average of selected dataset and multiplied by 7 (1 week).  

20 over 2 consecutive weeks is the current limit set as acceptable by Norwegian authorities for returning to Norway without a quarantine.

https://www.fhi.no/en/news/2020/grenseapning-i-forbindelse-med-covid-19/

The numbers behind the guide from Norwegian authorities shows average over two weeks for Scandinavia and sum of two weeks for the rest of Europe:
https://www.fhi.no/contentassets/0c17dede311e4e8f823a0e021d1f3ad0/2020.07.10-tallgrunnlag-norden-og-europa.xlsx

Numbers in these reports show per week.  Sum of two weeks must be less then 20 or average over two weeks must be less than 10.

## CoronaSummerStatus
Run console application and data is exported using csv (with ';' as delimiter) for countries selected in program.cs

## ExportToMarkdown
Expot a new markdown file in the [Results folder](https://github.com/Glattetre/CronaDataParser/tree/master/Results).

# Results

Please find results for a list of selected countries [here](https://github.com/Glattetre/CronaDataParser/tree/master/Results).
