import { useState } from "react";
import type { Book } from "./types";

type BookForm = {
  title: string;
  author: string;
  owner: string;
  isbn: string;
  publishedDate: string;
};

type Props = {
  editBook: Book | null;
  onConfirm: () => void;
  onClose: () => void;
};

// Regex for ISBN-13 format - I stole this from somewhere - to this date and to my shame I still suck at regex expressions
// Also, note, I'm not bothering to validate on the BE.  This is just a text field, and the API will accept any string.  In a real app, we would want to validate this on the BE as well, and not rely on the FE validation.
const ISBN_PATTERN = /^(978|979)-\d{10}$/;

export function AddEditBookModal({ editBook, onConfirm, onClose }: Props) {
  // So, I was having a few issues with my data validation between the FE and the BE (just fields I'd marked as required in the db model, and not in the FE form)
  // So I decided to display the error.  Initially in the console, then I thought I'd do it in a state.
  // But I'm lazy and this is a prototype so I'm just dumping the whole error object as a string.  In a real app, we would want to handle this more gracefully and display user friendly messages.
  const [error, setError] = useState<string | null>(null);
  const [form, setForm] = useState<BookForm>({
    title: editBook?.title || "",
    author: editBook?.author || "",
    owner: editBook?.owner || "",
    isbn: editBook?.isbn || "",
    publishedDate: editBook?.publishedDate || "",
  });

  // Call the state setter function by passing in an arrow function
  // that receives the previous state, set's the new state to the previous state, except for the field that changed (e.target.name) which is set to the new value (e.target.value).
  function handleField(e: React.ChangeEvent<HTMLInputElement>) {
    setForm((prev) => ({ ...prev, [e.target.name]: e.target.value }));
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    if (editBook) {
      // Edit book
      const res = await fetch(`/api/books/${editBook.id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          title: form.title,
          author: form.author,
          owner: form.owner,
          isbn: form.isbn || null,
          publishedDate: form.publishedDate || null,
        }),
      });
      if (res.ok) {
        onConfirm();
      } else {
        const body = await res.json();
        setError(JSON.stringify(body));
      }
    } else {
      // Add book
      const res = await fetch("/api/books", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          title: form.title,
          author: form.author,
          owner: form.owner,
          isbn: form.isbn || null,
          publishedDate: form.publishedDate || null,
        }),
      });
      if (res.ok) {
        onConfirm();
      } else {
        const body = await res.json();
        setError(JSON.stringify(body));
      }
    }
  }

  return (
    <div className="fixed inset-0 bg-black/50 flex items-center justify-center">
      <div className="bg-white rounded-lg p-6 w-96 shadow-xl">
        <h2 className="text-lg font-semibold mb-4">
          {editBook ? "Edit Book" : "Add Book"}
        </h2>
        <form onSubmit={handleSubmit} className="flex flex-col gap-3">
          <label className="text-sm">
            Title *
            <input
              name="title"
              value={form.title}
              onChange={handleField}
              required
              autoFocus
              className="mt-1 w-full border border-gray-300 rounded px-2 py-1"
            />
          </label>
          <label className="text-sm">
            Author *
            <input
              name="author"
              value={form.author}
              onChange={handleField}
              required
              className="mt-1 w-full border border-gray-300 rounded px-2 py-1"
            />
          </label>
          <label className="text-sm">
            Owner *
            <input
              name="owner"
              value={form.owner}
              onChange={handleField}
              required
              className="mt-1 w-full border border-gray-300 rounded px-2 py-1"
            />
          </label>
          <label className="text-sm">
            ISBN
            <input
              name="isbn"
              value={form.isbn}
              onChange={handleField}
              pattern={ISBN_PATTERN.source}
              className="mt-1 w-full border border-gray-300 rounded px-2 py-1 invalid:border-red-400"
            />
            {form.isbn && !ISBN_PATTERN.test(form.isbn) && (
              <p className="text-red-500 text-xs mt-1">
                ISBN-13 format required, e.g. 978-0061054884
              </p>
            )}
          </label>
          <label className="text-sm">
            Published Date
            <input
              type="date"
              name="publishedDate"
              value={form.publishedDate}
              onChange={handleField}
              // Set max to today's date to prevent future dates and bizarre dates
              max={new Date().toISOString().split("T")[0]}
              className="mt-1 w-full border border-gray-300 rounded px-2 py-1"
            />
          </label>
          {error && <p className="text-red-600 text-sm break-all">{error}</p>}
          <div className="flex justify-end gap-2 mt-2">
            <button
              type="button"
              onClick={onClose}
              className="px-4 py-1 rounded border border-gray-300 text-sm"
            >
              Cancel
            </button>
            <button
              type="submit"
              className="px-4 py-1 rounded bg-blue-600 text-white text-sm hover:bg-blue-700"
            >
              {editBook ? "Save Changes" : "Add Book"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
