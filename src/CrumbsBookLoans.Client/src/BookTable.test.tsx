import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { it, expect, vi, describe } from "vitest";
import { BookTable } from "./BookTable";
import type { Book } from "./types";

const availableBook: Book = {
  id: 1,
  title: "Downbelow Station",
  author: "C.J. Cherryh",
  owner: "Rob",
  isbn: "978-0879977382",
  publishedDate: "1981-09-01",
  loanStatus: "available",
  borrower: null,
};

const borrowedBook: Book = {
  ...availableBook,
  id: 2,
  loanStatus: "borrowed",
  borrower: "Lukey",
};

function renderTable(books: Book[], overrides = {}) {
  return render(
    <BookTable
      books={books}
      onBorrow={vi.fn()}
      onReturn={vi.fn()}
      onEdit={vi.fn()}
      onDelete={vi.fn()}
      {...overrides}
    />,
  );
}

describe("BookTable", () => {
  it("shows the book title and owner", () => {
    renderTable([availableBook]);
    expect(screen.getByText("Downbelow Station")).toBeInTheDocument();
    expect(screen.getByText("Rob")).toBeInTheDocument();
  });

  it("shows Available for an available book", () => {
    renderTable([availableBook]);
    expect(screen.getByText("Available")).toBeInTheDocument();
  });

  it("shows borrow button for an available book", () => {
    renderTable([availableBook]);
    expect(screen.getByTitle("Borrow book")).toBeInTheDocument();
    expect(screen.queryByTitle("Return book")).not.toBeInTheDocument();
  });

  it("calls onBorrow with the book when borrow is clicked", async () => {
    const onBorrow = vi.fn();
    renderTable([availableBook], { onBorrow });
    await userEvent.click(screen.getByTitle("Borrow book"));
    expect(onBorrow).toHaveBeenCalledWith(availableBook);
  });

  it("shows Borrowed by name for a borrowed book", () => {
    renderTable([borrowedBook]);
    expect(screen.getByText("Borrowed by Lukey")).toBeInTheDocument();
  });

  it("shows return button for a borrowed book", () => {
    renderTable([borrowedBook]);
    expect(screen.getByTitle("Return book")).toBeInTheDocument();
    expect(screen.queryByTitle("Borrow book")).not.toBeInTheDocument();
  });
});
