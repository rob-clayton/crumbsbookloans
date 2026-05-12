import { BookOpen, Trash2, Undo2, Pencil } from "lucide-react";
import type { Book } from "./types";

type Props = {
  books: Book[];
  onBorrow: (book: Book) => void;
  onReturn: (book: Book) => void;
  onDelete: (book: Book) => void;
  onEdit: (book: Book) => void;
};

export function BookTable({
  books,
  onBorrow,
  onReturn,
  onDelete,
  onEdit,
}: Props) {
  return (
    <table className="w-full text-left text-sm border-collapse">
      <thead>
        <tr className="border-b border-gray-200">
          <th className="pb-2 font-semibold">Book</th>
          <th className="pb-2 font-semibold">Owner</th>
          <th className="pb-2 font-semibold">Availability</th>
          <th className="pb-2 font-semibold">Actions</th>
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
            <td className="py-2">
              <div className="flex items-center gap-3">
                {book.loanStatus === "borrowed" ? (
                  <button
                    onClick={() => onReturn(book)}
                    title="Return book"
                    className="text-gray-500 hover:text-blue-600"
                  >
                    <Undo2 size={16} />
                  </button>
                ) : (
                  <button
                    onClick={() => onBorrow(book)}
                    title="Borrow book"
                    className="text-gray-500 hover:text-blue-600"
                  >
                    <BookOpen size={16} />
                  </button>
                )}
                <button
                  onClick={() => onEdit(book)}
                  title="Edit book"
                  className="text-gray-500 hover:text-blue-600"
                >
                  <Pencil size={16} />
                </button>
                <button
                  onClick={() => onDelete(book)}
                  title="Delete book"
                  className="text-gray-500 hover:text-red-600"
                >
                  <Trash2 size={16} />
                </button>
              </div>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
