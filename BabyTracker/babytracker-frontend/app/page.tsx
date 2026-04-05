import Image from "next/image";

export default function Home() {
  return (
    <div className="babyTrackerWrapper">
      <div className="flex justify-center mt-16">
        <table className="border">
          <thead>
            <tr className="border">
              <th className="py-2 px-3 text-center border">Time</th>
              <th className="py-2 px-3 text-center border">Milk</th>
              <th className="py-2 px-3 text-center border">Food</th>
              <th className="py-2 px-3 text-center border">Food Amount</th>
              <th className="py-2 px-3 text-center border">Poop</th>
            </tr>
          </thead>
          <tbody>
            {Array.from({ length: 24 }, (_, i) => (
              <tr key={i}>
                <td className="py-2 px-2 text-center border">
                  {String(i).padStart(2, "0")}
                </td>
                <td className="py-2 px-2 text-center border"></td>
                <td className="py-2 px-2 text-center border"></td>
                <td className="py-2 px-2 text-center border"></td>
                <td className="py-2 px-2 text-center border"></td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
