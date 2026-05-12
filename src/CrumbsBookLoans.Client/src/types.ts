export type Book = {
  id: number;
  title: string;
  author: string;
  isbn: string | null;
  publishedDate: string | null;
  owner: string;
  loanStatus: "available" | "borrowed";
  borrower: string | null;
};
