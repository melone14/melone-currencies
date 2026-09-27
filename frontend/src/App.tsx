import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { CurrenciesProvider } from './context/CurrenciesContext';
import CurrenciesView from './views/CurrenciesView';
import CurrencyView from './views/CurrencyView';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route
          path="/"
          element={
            <CurrenciesProvider>
              <CurrenciesView />
            </CurrenciesProvider>
          }
        />
        <Route path="/currencies/:code" element={<CurrencyView />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
