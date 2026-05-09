import { useEffect, useRef, useState } from "react";
import { AddBookModal } from "./AddBookModal";
import { BookTable } from "./BookTable";
import type { Book } from "./types";

// Fallback row height until we can measure it from the DOM.
const ROW_HEIGHT = 36;
// Minimum page size to avoid craziness on very small viewports.
const MIN_PAGE_SIZE = 5;

function App() {
  const [books, setBooks] = useState<Book[]>([]);
  const [page, setPage] = useState(1);
  const [search, setSearch] = useState("");
  const [showAddBookModal, setShowAddBookModal] = useState(false);
  const [pageSize, setPageSize] = useState(10);
  const [loadError, setLoadError] = useState<string | null>(null);
  const bookTableContainerRef = useRef<HTMLDivElement>(null);

  // Load books from the API and set them in state. Called on initial mount and after adding a book.
  function loadBooks() {
    // We are casting to Book[], but this is compile time so actually not checked at runtime. In a real app, we would want to validate this data before using it.
    fetch("/api/books")
      .then((res) => {
        if (!res.ok) throw new Error(`${res.status} ${res.statusText}`);
        return res.json();
      })
      .then((data: unknown) => {
        setBooks(data as Book[]);
        setLoadError(null);
      })
      .catch((err: unknown) => {
        setLoadError(
          err instanceof Error ? err.message : "Failed to load books",
        );
      });
  }

  // Load books on startup
  useEffect(() => {
    loadBooks();
  }, []);

  // Recalculate how many rows fit in the table container whenever it resizes
  // Use the semi magic ResizeObserver API to watch for changes to the size of the table container, and recalculate how many rows can fit based on the height of the container and the height of a row.
  useEffect(() => {
    const container = bookTableContainerRef.current;
    if (!container) return;

    const observer = new ResizeObserver(() => {
      // Get the first row and calculate how many rows can fit in the container based on its height.
      // If the list is empty, we won't have a row to measure, so we fall back to a default row height.
      // Took me a while to realize we had to include the thead height in the calculation as well, since that takes up vertical space and affects how many rows can fit.
      const firstRow = container.querySelector("tbody tr");
      const rowHeight = firstRow?.clientHeight ?? ROW_HEIGHT;
      const thead = container.querySelector("thead");
      const theadHeight = thead?.clientHeight ?? 0;
      const rows = Math.floor(
        (container.clientHeight - theadHeight) / rowHeight,
      );
      setPageSize(Math.max(rows, MIN_PAGE_SIZE));
    });

    observer.observe(container);
    // On unmount disconnect observer ... Does this actually leak if left out?
    return () => observer.disconnect();
  }, []);

  const filteredBooks = books.filter((book) =>
    book.title.toLowerCase().includes(search.toLowerCase()),
  );

  const totalPages = Math.ceil(filteredBooks.length / pageSize);
  const start = (page - 1) * pageSize;
  const pageBooks = filteredBooks.slice(start, start + pageSize);

  return (
    <div className="flex flex-col h-screen p-6">
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
            {start + 1}–{Math.min(start + pageSize, filteredBooks.length)}
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

      <div ref={bookTableContainerRef} className="flex-1 overflow-hidden">
        {loadError ? (
          <p className="text-red-600 text-sm mt-4">
            Could not load books: {loadError}
          </p>
        ) : (
          <BookTable books={pageBooks} />
        )}
      </div>

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
