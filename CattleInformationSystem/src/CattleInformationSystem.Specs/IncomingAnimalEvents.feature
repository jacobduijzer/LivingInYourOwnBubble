Feature: Incoming Animal Events
Animal events, coming from the Netherlands Enterprise Agency (RVO) should be processed into the database.
TODO: work with real ubn's and life numbers?

    Background: Being Born
        Given a farm of type 'BreedingForMilk', with UBN '21234567890'
        Given an animal, born today
          | Key         | Value       |
          | Life Number | 10987654321 |
          | Gender      | Female      |
          | Ubn         | 21234567890 |
          | Reason      | Birth       |
          | EventDate   | 2017-03-23  |
        And it is added to the incoming events table

    Scenario: Being born
        Then it should be processed and stored in the database
        And have the events(s)
          | Farm        | Reason | Order | EventDate  | Category |
          | 21234567890 | Birth  | 1     | 2017-03-23 | 101      |

    Scenario: Sold and bought
        When it is sold today, to a UBN of type 'Milk'
        Then it should be processed and stored in the database
        And have the events(s)
          | Farm            | Reason    | Order | Date  | Category |
          | BreedingForMilk | Birth     | 1     | Today | 101      |
          | BreedingForMilk | Departure | 2     | Today | 101      |
          | Milk            | Arrival   | 1     | Today | 101      |