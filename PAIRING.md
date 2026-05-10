# Pairing Session – Improvement Ideas

These are talking points and extension ideas for the follow-up pair programming interview.
They are not part of the submitted prototype.

---

## Design Decisions to Discuss

* **Loan/return as explicit endpoints vs generic update** — I consider these actions domain actions, not pure updates, hence the post endpoints rather than a single put.
* **React Query** — opted for plain `fetch` with local state; React Query would be the natural next step if caching or background refetch became necessary
