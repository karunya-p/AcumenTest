Feature: AcumenContactUs
	In order to contact Acumen
	As an end user
	I want to be able to find contact details of Acumen and get in touch to find out more

	#Accurate Address validation -------------------------------------------------------------------------
	Scenario: Accurate Address
	Given I am on the Acumen home page
	When I navigate to the Contact Us page
	Then I should see an accurate address as follows
	| CompanyName                    | StreetAddress    | Town                 | CountyWithPostCode | Country        | ClientEnquiresTelephone  | ClientEnquiresEmail | SupportEnquiresTelephone | SupportEnquiresEmail |
	| Acumen Commercial Insights Ltd | 26 George Street | Richmond upon Thames | Surrey TW9 1HY     | UNITED KINGDOM | Tel: +44 (0)20 8334 0420 | info@acumenci.com   | Tel: +44 (0)20 8334 0430 | support@acumenci.com |

	# Happy path form submission ------------------------------------------------------------------------
	Scenario: ContactUs form successful submission Happy path
	Given I am on the Acumen Contact Us page
	When I submit the contact form with the following details below
	| Name     | Company     | Location | Telephone | Email                 | Subject | YourMessage        |
	| SomeName | SomeCompany | London   |           | testKarunya@gmail.com |         | Please contact me! |
	Then I should see successful submission message


	#Required field validations --------------------------------------------------------------------------
	Scenario: Required field Validation with EmailAddress only
	Given I am on the Acumen Contact Us page
	When I submit the contact form with the following details below
	| Name | Company | Location | Telephone | Email                 | Subject | YourMessage        |
	|      |         |          |           | testKarunya@gmail.com |         | Please contact me! |
	Then I should see errors about the other required fields that are not filled

	#Email format validations ---------------------------------------------------------------------------

	Scenario: Email format Validation for email with no domain name and atsign
	Given I am on the Acumen Contact Us page
	When I submit the contact form with the following details below
	| Name     | Company     | Location | Telephone | Email       | Subject | YourMessage        |
	| SomeName | SomeCompany | London   |           | testKarunya |         | Please contact me! |
	Then I should see validation error message 'Email address seems invalid.'

	Scenario: Email format Validation for email with sometext and atSign but no domain
	Given I am on the Acumen Contact Us page
	When I submit the contact form with the following details below
	| Name     | Company     | Location | Telephone | Email        | Subject | YourMessage        |
	| SomeName | SomeCompany | London   |           | testKarunya@ |         | Please contact me! |
	Then I should see validation error message 'Email address seems invalid.'

	Scenario: Email format Validation for email with sometext and atSign and domain with no dot 
	Given I am on the Acumen Contact Us page
	When I submit the contact form with the following details below
	| Name     | Company     | Location | Telephone | Email             | Subject | YourMessage        |
	| SomeName | SomeCompany | London   |           | testKarunya@gmail |         | Please contact me! |
	Then I should see validation error message 'Email address seems invalid.'

	Scenario: Email format Validation for email with notext after the dot
	Given I am on the Acumen Contact Us page
	When I submit the contact form with the following details below
	| Name     | Company     | Location | Telephone | Email              | Subject | YourMessage        |
	| SomeName | SomeCompany | London   |           | testKarunya@gmail. |         | Please contact me! |
	Then I should see validation error message 'Email address seems invalid.'

	Scenario: Email format success case
	Given I am on the Acumen Contact Us page
	When I submit the contact form with the following details below
	| Name | Company     | Location | Telephone | Email                 | Subject | YourMessage        |
	|      | SomeCompany | London   |           | testKarunya@gmail.com |         | Please contact me! |	
	Then I should see no errors in the email field
	
	#Google map -----------------------------------------------------------------------------------------
	Scenario: Google map available
	Given I am on the Acumen Contact Us page
	Then I should be able to see Google map widget showing address

	#How to find us ------------------------------------------------------------------------------------
	Scenario: How to find us is described
	Given I am on the Acumen Contact Us page
	Then I should see how to find us section 

