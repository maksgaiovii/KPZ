import { endpoinstName } from "../../config";
import { useTab } from "../../context/tab";

const linkStyles = [
  "inline-block p-4 border-b-2 border-transparent rounded-t-lg hover:text-gray-600 hover:border-gray-300 dark:hover:text-gray-300",
  "inline-block p-4 text-blue-600 border-b-2 border-blue-600 rounded-t-lg active dark:text-blue-500 dark:border-blue-500",
];

export const Tabs = () => {
  const { tab, setTab } = useTab();

  return (
    <div className="text-sm font-medium text-center text-gray-500 border-b border-gray-200 dark:text-gray-400 dark:border-gray-700">
      <ul className="flex flex-wrap -mb-px">
        {endpoinstName.map(({ endpoint, tabName }) => (
          <li
            className={
              "me-2 " + (tab !== endpoint ? linkStyles[0] : linkStyles[1])
            }
            onClick={() => setTab(endpoint)}
          >
            {tabName}
          </li>
        ))}
      </ul>
    </div>
  );
};
