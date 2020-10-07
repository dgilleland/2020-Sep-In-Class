Constraint Prefixes:

- CK - check
- PK - Primary Key
- FK - Foreign Key
- DF - Default
- IX - Indexes
- UX - Unique

## Practice

Add the following check constraints:

- Credits can only be 3, 4.5, or 6; use the `IN` operator.
- The `Courses.Number` must follow the pattern of "AAAA-####"
- Year must be greater than 2010
- Surname must have at least two characters
- GivenName must have at least two characters
- FinalMark must be between 0 and 100
- Status must be either 'E' for Enrolled, 'W' for withdrawn, or 'A' for Audited

Imagine a CHECK constraint like this:

- 60/90 hrs = 3/4.5 credits OR 120 hrs = 6 credits