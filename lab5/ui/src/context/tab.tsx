import { createContext, useContext } from "react";
import { useSearchParams } from "react-router-dom";

interface ITabContext {
  tab: string;
  setTab: (tab: string) => void;
}

const TabContext = createContext<ITabContext>({
  tab: "books",
  setTab: () => {},
});

const TabProvider = ({ children }: { children: React.ReactNode }) => {
  const [searchParams, setSearchParams] = useSearchParams();
  const tab = searchParams.get("tab") || "";

  const setTab = (newTab: string) => {
    setSearchParams({ tab: newTab });
  };

  return (
    <TabContext.Provider value={{ tab, setTab }}>
      {children}
    </TabContext.Provider>
  );
};

const useTab = () => useContext(TabContext);

export { TabProvider, useTab };
