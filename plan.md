# Tempus

Example project for time billing using Minimal APIs and Microservice Architecture.

## Stories

- User adds a time bill for work performed
- User creates a project to allow for time bills to be assigned
- User creates customers for which projects are assigned
- User creates invoice based on time bills for specific customer
- User accepts payment to show that invoice is paid
- User applies credits to an invoice
- User voids an invoice
- User deletes a time bill
- User creates statements for past-due invoices
- User sets up types of work that has default price per hour
- User creates workers who can make time bills
- User can email invoice to customer
- User can print invoice for customer

## Data required

- Customers
- Projects
- TimeBills
- WorkTypes
- Invoices
- Payments
- Workers
- InvoiceCredits

## Services

- CustomerService
- InvoiceService
- PaymentService
- WorkerService
- TimeBillingService
- WorkTypeService
- IdentityService
- EmailService

