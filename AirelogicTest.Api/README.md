# Panda

You can reset the database by deleting the `panda.db` file, it will regenerate on the next run. It will add as empty.

Rules I followed:
- Vertical slice architecture
- No comments in code - code should explain itself.


Criticism of the code:
- I used SQLite for the first time so did not set up migrations.
- SQLite lacks a lot of types compared to other databases, so I've used strings a lot.
- I used NHSNumber as a primary key

Considerations:
- Dedicated models project.
- Swagger.