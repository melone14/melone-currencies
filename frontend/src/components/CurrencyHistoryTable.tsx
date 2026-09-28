import type { CurrencyHistory } from '../api/currencies';

interface CurrencyHistoryTableProps {
  history: CurrencyHistory;
}

function CurrencyHistoryTable({ history }: CurrencyHistoryTableProps) {
  return (
    <div style={{ width: '600px', margin: '20px auto' }}>
      <h3>Historia kursu: {history.code} - {history.name}</h3>
      <table style={{ width: '100%', borderCollapse: 'collapse' }}>
        <thead>
          <tr>
            <th style={{ textAlign: 'left', borderBottom: '1px solid #ccc', padding: '4px 8px' }}>Data</th>
            <th style={{ textAlign: 'right', borderBottom: '1px solid #ccc', padding: '4px 8px' }}>Kurs</th>
          </tr>
        </thead>
        <tbody>
          {history.rates.map((point) => (
            <tr key={point.date}>
              <td style={{ padding: '4px 8px' }}>{new Date(point.date).toLocaleDateString('pl-PL')}</td>
              <td style={{ textAlign: 'right', padding: '4px 8px' }}>{point.value}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default CurrencyHistoryTable;