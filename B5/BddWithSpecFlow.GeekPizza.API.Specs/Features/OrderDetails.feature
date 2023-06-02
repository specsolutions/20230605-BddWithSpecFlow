﻿@webapi
Feature: Order Details

@login
Scenario: Pizza is ordered for today by default
	Given the client has items in the basket
	When the client checks the my order page
	Then the order should indicate that the delivery date is today

#B5: Make sure the following scenario is not only tested for tomorrow/14:00, 
#    but for further dates and times as well as listed in the workbook!
@login
Scenario: Pizza is ordered for tomorrow
	Given the client has items in the basket
	When the client specifies tomorrow at 14:00 as delivery time
	Then the order should indicate that the delivery date is tomorrow
	And the delivery time should be 14:00

@login
Scenario: Practice: Pizza is ordered for a different date
	Given the client has items in the basket
	When the client specifies 5 days later at 1pm as delivery time
	Then the order should indicate that the delivery date is 5 days later
