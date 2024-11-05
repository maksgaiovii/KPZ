import { createContext, useContext } from "react";
import { useSearchParams } from "react-router-dom";
import { TabType } from "../config";

interface ITabContext {
  tab: TabType;
  setTab: (tab: TabType) => void;
}

const TabContext = createContext<ITabContext>({
  tab: '' as TabType,
  setTab: () => {},
});

const TabProvider = ({ children }: { children: React.ReactNode }) => {
  const [searchParams, setSearchParams] = useSearchParams();
  const tab = (searchParams.get("tab") || "") as TabType;

  const setTab = (newTab: TabType) => {
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
