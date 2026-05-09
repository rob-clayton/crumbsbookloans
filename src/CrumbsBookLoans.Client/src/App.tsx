import { useEffect, useState } from "react";
import { AddBookModal } from "./AddBookModal";
import { BookTable } from "./BookTable";
import type { Book } from "./types";

const PAGE_SIZE = 10;

function App() {
  const [books, setBooks] = useState<Book[]>([]);
  const [page, setPage] = useState(1);
  const [search, setSearch] = useState("");
  const [showAddBookModal, setShowAddBookModal] = useState(false);

  // Load books from the API and set them in state. Called on initial mount and after adding a book.
  function loadBooks() {
    // Note: No real error handling
    // We are casting to Book[], but this is compile time so actually not checked at runtime. In a real app, we would want to validate this data before using it.
    fetch("/api/books")
      .then((res) => res.json())
      .then((data: unknown) => setBooks(data as Book[]));
  }

  // Load books on startup
  useEffect(() => {
    loadBooks();
  }, []);

  const filteredBooks = books.filter((book) =>
    book.title.toLowerCase().includes(search.toLowerCase()),
  );

  const totalPages = Math.ceil(filteredBooks.length / PAGE_SIZE);
  const start = (page - 1) * PAGE_SIZE;
  const pageBooks = filteredBooks.slice(start, start + PAGE_SIZE);

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold mb-4 text-center">Library</h1>

      <div className="flex justify-between items-center mb-2">
        <label className="text-sm">
          Book Search:&nbsp;
          <input
            type="text"
            value={search}
            onChange={(e) => {
              setSearch(e.target.value);
              setPage(1);
            }}
            className="border border-gray-300 rounded px-2 py-1"
          />
        </label>

        <div className="flex items-center gap-3 text-sm">
          <button
            onClick={() => setPage((p) => p - 1)}
            disabled={page === 1}
            className="disabled:opacity-40"
          >
            ←
          </button>
          <span>
            {start + 1}–{Math.min(start + PAGE_SIZE, filteredBooks.length)}
          </span>
          <button
            onClick={() => setPage((p) => p + 1)}
            disabled={page === totalPages}
            className="disabled:opacity-40"
          >
            →
          </button>
        </div>
      </div>

      <BookTable books={pageBooks} />

      {/* Floating add button — per spec, though under the table would feel more natural */}
      <button
        onClick={() => setShowAddBookModal(true)}
        className="fixed bottom-6 right-6 bg-blue-600 hover:bg-blue-700 text-white rounded px-4 py-2 text-sm shadow-lg"
      >
        Add Book
      </button>

      {showAddBookModal && (
        <AddBookModal
          onAdd={() => {
            // After adding a book, we want to refresh the list. In a real app, we might want to just add the new book to state instead of reloading everything.
            // I'm keeping it simple to ensure the refreshed data has the book ordered correctly in the list, and to avoid any potential issues with the new book data not matching what the API returns.
            // Double note: In a database of this size, indexing etc is actually an overhead ... but obviously not as size increases.
            loadBooks();
            setShowAddBookModal(false);
          }}
          onClose={() => setShowAddBookModal(false)}
        />
      )}
    </div>
  );
}

export default App;
