import { useState } from "react";

type Props = {
  onBorrow: (borrower: string) => void;
  onClose: () => void;
};

export function BorrowModal({ onBorrow, onClose }: Props) {
  const [borrower, setBorrower] = useState("");

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    onBorrow(borrower);
  }

  return (
    <div className="fixed inset-0 bg-black/50 flex items-center justify-center">
      <div className="bg-white rounded-lg p-6 w-80 shadow-xl">
        <h2 className="text-lg font-semibold mb-4">Borrow Book</h2>
        <form onSubmit={handleSubmit} className="flex flex-col gap-3">
          <label className="text-sm">
            Your name *
            <input
              value={borrower}
              onChange={(e) => setBorrower(e.target.value)}
              required
              className="mt-1 w-full border border-gray-300 rounded px-2 py-1"
            />
          </label>
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
              Borrow
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
