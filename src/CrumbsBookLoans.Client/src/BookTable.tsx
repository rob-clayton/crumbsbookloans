import type { Book } from "./types";

type Props = {
  books: Book[];
};

export function BookTable({ books }: Props) {
  return (
    <table className="w-full text-left text-sm border-collapse">
      <thead>
        <tr className="border-b border-gray-200">
          <th className="pb-2 font-semibold">Book</th>
          <th className="pb-2 font-semibold">Owner</th>
          <th className="pb-2 font-semibold">Availability</th>
        </tr>
      </thead>
      <tbody>
        {books.map((book) => (
          <tr key={book.id} className="border-b border-gray-100">
            <td className="py-2">{book.title}</td>
            <td className="py-2">{book.owner}</td>
            <td className="py-2">
              {book.loanStatus === "borrowed"
                ? `Borrowed by ${book.borrower}`
                : "Available"}
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
