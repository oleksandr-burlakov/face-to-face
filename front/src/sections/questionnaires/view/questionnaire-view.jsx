import { useState, useEffect } from 'react';

import Card from '@mui/material/Card';
import Stack from '@mui/material/Stack';
import Table from '@mui/material/Table';
import Button from '@mui/material/Button';
import Container from '@mui/material/Container';
import TableBody from '@mui/material/TableBody';
import Typography from '@mui/material/Typography';
import TableContainer from '@mui/material/TableContainer';
import TablePagination from '@mui/material/TablePagination';

import { getInfo } from 'src/api/account-api';
import { questionnaires } from 'src/_mock/questionnaires';

import Iconify from 'src/components/iconify';
import Scrollbar from 'src/components/scrollbar';

import TableNoData from '../table-no-data';
import TableEmptyRows from '../table-empty-rows';
import QuestionnaireTableRow from '../questionnaire-table-row';
import QuestionnaireTableHead from '../questionnaire-table-head';
import { emptyRows, applyFilter, getComparator } from '../utils';
import QuestionnaireFormModal from './modal/questionnaire-form-modal';
import QuestionnaireTableToolbar from '../questionnaire-table-toolbar';


// ----------------------------------------------------------------------

export default function QuestionnaireView() {
  const [page, setPage] = useState(0);

  const [order, setOrder] = useState('asc');

  const [selected, setSelected] = useState([]);

  const [orderBy, setOrderBy] = useState('name');

  const [filterName, setFilterName] = useState('');

  const [rowsPerPage, setRowsPerPage] = useState(5);

  const [openFormModal, setOpenFormModal] = useState(false);

  const [activeQuestionnaire, setActiveQuestionnaire] = useState();

  const handleSort = (event, id) => {
    const isAsc = orderBy === id && order === 'asc';
    if (id !== '') {
      setOrder(isAsc ? 'desc' : 'asc');
      setOrderBy(id);
    }
  };

  const handleSelectAllClick = (event) => {
    if (event.target.checked) {
      const newSelecteds = questionnaires.map((n) => n.name);
      setSelected(newSelecteds);
      return;
    }
    setSelected([]);
  };

  const handleClick = (event, name) => {
    const selectedIndex = selected.indexOf(name);
    let newSelected = [];
    if (selectedIndex === -1) {
      newSelected = newSelected.concat(selected, name);
    } else if (selectedIndex === 0) {
      newSelected = newSelected.concat(selected.slice(1));
    } else if (selectedIndex === selected.length - 1) {
      newSelected = newSelected.concat(selected.slice(0, -1));
    } else if (selectedIndex > 0) {
      newSelected = newSelected.concat(
        selected.slice(0, selectedIndex),
        selected.slice(selectedIndex + 1)
      );
    }
    setSelected(newSelected);
  };

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setPage(0);
    setRowsPerPage(parseInt(event.target.value, 10));
  };

  const handleFilterByName = (event) => {
    setPage(0);
    setFilterName(event.target.value);
  };

  useEffect(() => {
    async function fetchData() {
      let response = await getInfo();
      response = await response.json();
      console.log(response);
    }
    fetchData();
  }, []);

  const dataFiltered = applyFilter({
    inputData: questionnaires,
    comparator: getComparator(order, orderBy),
    filterName,
  });

  const closeModal = () => {
    setOpenFormModal(false);
    setActiveQuestionnaire(null);
  };

  const openModal = (questionnaire) => {
    setActiveQuestionnaire(questionnaire);
    setOpenFormModal(true);
  };

  const handleEditClick = (questionnaire) => {
    openModal(questionnaire);
  };

  const notFound = !dataFiltered.length && !!filterName;

  return (
    <Container>
      <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
        <Typography variant="h4">Questionnaires</Typography>

        <Button onClick={() => openModal()} variant="contained" color="inherit" startIcon={<Iconify icon="eva:plus-fill" />}>
          New Questionnaire 
        </Button>
        <QuestionnaireFormModal open={openFormModal} questionnaire={activeQuestionnaire}  handleClose={closeModal} />
      </Stack>

      <Card>
      <QuestionnaireTableToolbar
          numSelected={selected.length}
          filterName={filterName}
          onFilterName={handleFilterByName}
        />

        <Scrollbar>
          <TableContainer sx={{ overflow: 'unset' }}>
            <Table sx={{ minWidth: 800 }}>
              <QuestionnaireTableHead
                order={order}
                orderBy={orderBy}
                rowCount={questionnaires.length}
                numSelected={selected.length}
                onRequestSort={handleSort}
                onSelectAllClick={handleSelectAllClick}
                headLabel={[
                  { id: 'title', label: 'Title' },
                  { id: '' },
                ]}
              />
              <TableBody>
                {dataFiltered
                  .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                  .map((row) => (
                    <QuestionnaireTableRow
                      key={row.id}
                      questionnaire={row}
                      selected={selected.indexOf(row.title) !== -1}
                      handleEditClick={handleEditClick}
                      handleClick={(event) => handleClick(event, row.title)}
                    />
                  ))}

                <TableEmptyRows
                  height={77}
                  emptyRows={emptyRows(page, rowsPerPage, questionnaires.length)}
                />

                {notFound && <TableNoData query={filterName} />}
              </TableBody>
            </Table>
          </TableContainer>
        </Scrollbar>

        <TablePagination
          page={page}
          component="div"
          count={questionnaires.length}
          rowsPerPage={rowsPerPage}
          onPageChange={handleChangePage}
          rowsPerPageOptions={[5, 10, 25]}
          onRowsPerPageChange={handleChangeRowsPerPage}
        />
      </Card>
    </Container>
  );
}
