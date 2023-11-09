import { useNavigate } from 'react-router-dom';
import React, { useState, useEffect, SetStateAction } from 'react';

import Card from '@mui/material/Card';
import Stack from '@mui/material/Stack';
import Table from '@mui/material/Table';
import Button from '@mui/material/Button';
import Container from '@mui/material/Container';
import TableBody from '@mui/material/TableBody';
import Typography from '@mui/material/Typography';
import TableContainer from '@mui/material/TableContainer';
import TablePagination from '@mui/material/TablePagination';

import { timeErrorAlert } from 'src/utils/helpers/alert-helper';

import { GetMyQuestionnaireModelType } from 'src/models/questionnaire/'
import { deleteQuestionnaire, getMyQuestionnaires } from 'src/api/questionnaire-api';

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
  const [questionnaires, setQuestionnaires] = useState<GetMyQuestionnaireModelType[]>([]);
  const [page, setPage] = useState(0);

  const [order, setOrder] = useState<'asc'|'desc'>('asc');

  const [selected, setSelected] = useState<string[]>([]);

  const [orderBy, setOrderBy] = useState('name');

  const [filterName, setFilterName] = useState('');

  const [rowsPerPage, setRowsPerPage] = useState(5);

  const [openFormModal, setOpenFormModal] = useState(false);

  const [activeQuestionnaire, setActiveQuestionnaire] = useState<GetMyQuestionnaireModelType | null>();

  const navigate = useNavigate();

  const handleSort = (event: any, id: string) => {
    const isAsc = orderBy === id && order === 'asc';
    if (id !== '') {
      setOrder(isAsc ? 'desc' : 'asc');
      setOrderBy(id);
    }
  };

  const handleSelectAllClick = (event: any) => {
    if (event.target.checked) {
      const newSelecteds = questionnaires.map((n) => n.title);
      setSelected(newSelecteds);
      return;
    }
    setSelected([]);
  };

  const handleClick = (event: any, name: string) => {
    const selectedIndex = selected.indexOf(name);
    let newSelected: string[] = [];
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

  const handleChangePage = (event: any, newPage: SetStateAction<number>) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event: any) => {
    setPage(0);
    setRowsPerPage(parseInt(event.target.value, 10));
  };

  const handleFilterByName = (event: any) => {
    setPage(0);
    setFilterName(event.target.value);
  };

  useEffect(() => {
    loadQuestionnaires();
  }, [setQuestionnaires]);

  const dataFiltered = applyFilter({
    inputData: questionnaires,
    comparator: getComparator(order, orderBy),
    filterName,
  });

  const loadQuestionnaires = async () =>  {
    await getMyQuestionnaires()
      .catch((reason) => {
        timeErrorAlert(reason.response.data.Errors[0]);
      })
      .then((request) => {
        if (request) {
          if (request.data.succeeded) {
            setQuestionnaires(request.data.result);
          }
        }
      });
  }

  const closeModal = async () => {
    setOpenFormModal(false);
    setActiveQuestionnaire(null);
    await loadQuestionnaires();
  };

  const openModal = (questionnaire?: GetMyQuestionnaireModelType) => {
    setActiveQuestionnaire(questionnaire);
    setOpenFormModal(true);
  };

  const handleEditClick = (questionnaire?: GetMyQuestionnaireModelType) => {
    openModal(questionnaire);
  };

  const handleDeleteClick = async (questionnaire: GetMyQuestionnaireModelType) => {
    await deleteQuestionnaire(questionnaire.id);
    setQuestionnaires(questionnaires.filter(q => q.id !== questionnaire.id));
  };

  const openQuestionnaireDetails = (questionnaire: GetMyQuestionnaireModelType) => {
    navigate(`${questionnaire.id}`);
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
                  .map((row: GetMyQuestionnaireModelType) => (
                    <QuestionnaireTableRow
                      key={row.id}
                      handleTableRowClick={openQuestionnaireDetails}
                      questionnaire={row}
                      selected={selected.indexOf(row.title) !== -1}
                      handleEditClick={handleEditClick}
                      handleDeleteClick={handleDeleteClick}
                      handleClick={(event: any) => handleClick(event, row.title)}
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
