// Copyright (c) Thomas Nieto - All Rights Reserved
// You may use, distribute and modify this code under the
// terms of the MIT license.

using System.Data;
using System.Management.Automation;
using System.Text.RegularExpressions;

using AnyPackage.Provider;

namespace AnyPackage.Tui;

public partial class AnyPackageView
{
    private IEnumerable<PackageInfo> _getPackage = Array.Empty<PackageInfo>();
    private IEnumerable<PackageInfo> _findPackage = Array.Empty<PackageInfo>();

    public AnyPackageView()
    {
        InitializeComponent();
        Refresh();

        filterTextField.TextChanged += (str) =>
        {
            var filter = filterTextField.Text.ToString();

            if (string.IsNullOrWhiteSpace(filter))
            {
                SetPackages(getTableView.Table, _getPackage);
                SetPackages(findTableView.Table, _findPackage);
            }
            else
            {
                FilterPackages(getTableView.Table, _getPackage, filter);
                FilterPackages(findTableView.Table, _findPackage, filter);
            }

            getTableView.Update();
            findTableView.Update();
            updateTableView.Update();
        };
    }

    private void FilterPackages(DataTable table, IEnumerable<PackageInfo> packages, string filter)
    {
        var regexOptions = RegexOptions.IgnoreCase;
        var filteredPackages = packages.Where(x => Regex.IsMatch(x.Name, filter, regexOptions)
                                                   || Regex.IsMatch(x.Version?.ToString() ?? "", filter, regexOptions)
                                                   || Regex.IsMatch(x.Source?.Name ?? "", filter, regexOptions)
                                                   || Regex.IsMatch(x.Provider.Name, filter, regexOptions));
        SetPackages(table, filteredPackages);
    }

    private void SetSources()
    {
        sourceTableView.Table.Clear();

        var sources = PowerShell.Create(RunspaceMode.CurrentRunspace)
                                 .AddCommand($"Get-PackageSource")
                                 .Invoke<PackageSourceInfo>();

        foreach (var source in sources)
        {
            sourceTableView.Table.Rows.Add(source.Name, source.Location, source.Trusted, source.Provider);
        }
    }

    private void SetPackages(DataTable table, string verb)
    {
        table.Clear();
        var packages = PowerShell.Create(RunspaceMode.CurrentRunspace)
                                 .AddCommand($"{verb}-Package")
                                 .Invoke<PackageInfo>();

        if (verb == "Get")
        {
            _getPackage = packages;
        }
        else if (verb == "Find")
        {
            _findPackage = packages;
        }

        foreach (var package in packages)
        {
            table.Rows.Add(package.Name, package.Version, package.Source, package.Provider);
        }
    }

    private void SetPackages(DataTable table, IEnumerable<PackageInfo> packages)
    {
        table.Clear();

        foreach (var package in packages)
        {
            table.Rows.Add(package.Name, package.Version, package.Source, package.Provider);
        }
    }

    private void SetProviders()
    {
        providerTableView.Table.Clear();

        var providers = PowerShell.Create(RunspaceMode.CurrentRunspace)
                                 .AddCommand($"Get-PackageProvider")
                                 .Invoke<PackageProviderInfo>();

        foreach (var provider in providers)
        {
            providerTableView.Table.Rows.Add(provider.Name, provider.Version, provider.Priority, provider.Operations);
        }
    }

    internal void Refresh()
    {
        SetPackages(getTableView.Table, "Get");
        SetPackages(findTableView.Table, "Find");
        SetSources();
        SetProviders();
        tabView.SelectedTab = tabView.SelectedTab;
    }
}
