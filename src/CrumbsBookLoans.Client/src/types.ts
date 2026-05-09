export type Book = {
  id: number;
  title: string;
  author: string;
  owner: string;
  loanStatus: "available" | "borrowed";
  borrower: string | null;
};
